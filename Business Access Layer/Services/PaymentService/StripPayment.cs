using BusinessAccessLayer.DTOS;
using DataAccessLayer.Data.Models;
using DataAccessLayer.UnitOfWorkRepo;
using Stripe.Checkout;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BusinessAccessLayer.Services.PaymentService
{
    public class StripPayment : IPayment 
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        public StripPayment(IUnitOfWork unitOfWork , IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public async Task<PaymentResult> Payment(List<ShoppingCart> shoppingCart, int orderId)
        {           
            var domain = "https://localhost:7138/";
            // Set the Stripe API key
            StripeConfiguration.ApiKey = configuration["Stripe:PublishableKey"];
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),

                Mode = "payment",
                SuccessUrl = domain + $"api/Order/OrderConfirmation?orderid={orderId}",
                CancelUrl = domain + $"api/Order/cancelorder",
            };

            foreach (var item in shoppingCart)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.CurrentPrice * 100),
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        },
                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            if (session == null)
            {
                return new PaymentResult()
                {
                    status = false,
                    Massage = "wrong in complete payment"
                };
            }
            var result = await UpdateStripePaymentId(orderId, session.Id, session.PaymentIntentId);
            if (result == 0)
            {
                return new PaymentResult()
                {
                    status = false,
                    Massage = "wrong in complete payment"
                };
            }
            return new PaymentResult()
            {
                status = true,
                Massage = "complete payment",
                session = session.Url

            };
        }
        public string PaymentStatus(string sessionId)
        {
            var service = new SessionService();
            Session session = service.Get(sessionId);
            return session.PaymentStatus.ToLower();
        }
        public Session PaymentSession(string sessionId)
        {
            var service = new SessionService();
            Session session = service.Get(sessionId);
            return session;
        }
        public async Task<int> UpdateStripePaymentId(int orderId, string id, string paymentIntentId)
        {
            var order = await unitOfWork.orderRepo.GetByIdAsync(orderId);
            if (order == null) { return 0; }
            order.PaymentIntentId = paymentIntentId;
            order.SessionId = id;
            order.UpdateAt = DateTime.UtcNow;
            unitOfWork.orderRepo.Update(order,nameof(order.PaymentIntentId), nameof(order.SessionId),nameof(order.UpdateAt));
            var result = unitOfWork.Complete();
            return result;
        }
        public async Task<PaymentResult> CancelPayment(string paymentIntentId)
        {
            // Set the Stripe API key
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

            var service = new PaymentIntentService();
            try
            {
                PaymentIntent paymentIntent = await service.CancelAsync(paymentIntentId);
                if (paymentIntent.Status == "canceled")
                {
                    return new PaymentResult()
                    {
                        status = true,
                        Massage = "Payment successfully canceled"
                    };
                }
                else
                {
                    return new PaymentResult()
                    {
                        status = false,
                        Massage = "Failed to cancel the payment"
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new PaymentResult()
                {
                    status = false,
                    Massage = $"Error in canceling payment: {ex.Message}"
                };
            }
        }

        public async Task<PaymentResult> Payment1(long total , int orderId, CardPaymentRequest cardPaymentRequest)
        {
            try
            {
               // var domain = "https://localhost:7138/"; // Base domain for the application (used for success and cancel URLs).

                // Set the Stripe API key for authentication with the Stripe API.
                StripeConfiguration.ApiKey = configuration["Stripe:PublishableKey"];
                // Create a PaymentMethod using the card details provided in the CardPaymentRequest.
                var paymentMethodOptions = new PaymentMethodCreateOptions
                {
                    Type = "card", // Specify the type of payment method as a card.
                    Card = new PaymentMethodCardOptions
                    {
                        Number = cardPaymentRequest.CardNumber, // Card number provided by the user.
                        ExpMonth = cardPaymentRequest.ExpMonth, // Card's expiration month.
                        ExpYear = cardPaymentRequest.ExpYear, // Card's expiration year.
                        Cvc = cardPaymentRequest.CVC, // Card's CVC code.
                    },
                };

                // Initialize the PaymentMethodService to create a new payment method in Stripe.
                var paymentMethodService = new PaymentMethodService();
                var paymentMethod = await paymentMethodService.CreateAsync(paymentMethodOptions); // Create the payment method asynchronously.

                // Create a PaymentIntent to handle the payment using the previously created PaymentMethod.
                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = total, // The total amount to be charged, in the smallest currency unit (e.g., cents).
                    Currency = "egp", // Currency of the transaction (e.g., Egyptian Pounds).
                    PaymentMethod = paymentMethod.Id, // The ID of the created PaymentMethod.
                    ConfirmationMethod = "manual", // Specify that the confirmation will be done manually.
                    Confirm = true, // Automatically confirm the PaymentIntent upon creation.
                    Metadata = new Dictionary<string, string> // Additional metadata to include with the PaymentIntent.
                    {
                    { "orderId", orderId.ToString() } // Add the order ID to the metadata for tracking purposes.
                    }
                };

                // Initialize the PaymentIntentService to create and manage PaymentIntents.
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions); // Create the PaymentIntent asynchronously.

                // Check the status of the PaymentIntent to determine if the payment was successful.
                if (paymentIntent.Status == "succeeded")
                {
                    // If the payment was successful, update the payment details in the database.
                 await UpdateStripePaymentId(orderId, paymentIntent.Id, paymentIntent.Id);
        
                    return new PaymentResult
                    {
                        status = true,
                        Massage = "Payment completed successfully", // Indicate that the payment was completed successfully.
                        session = paymentIntent.Id // Include the PaymentIntent ID for tracking or debugging.
                    };
                }
                else if (paymentIntent.Status == "requires_action")
                {
                    // If the payment requires additional user action (e.g., 3D Secure authentication).
                    return new PaymentResult
                    {
                        status = false,
                        Massage = "Payment requires additional action. Please follow up with the customer." // Inform the user about the required action.
                    };
                }
                else
                {
                    // If the payment failed for any other reason.
                    return new PaymentResult
                    {
                        status = false,
                        Massage = "Payment failed. Please try again." // Inform the user about the failure.
                    };
                }
            }
            catch (StripeException ex) // Handle Stripe-specific exceptions.
            {
                // Return an error result with details about the Stripe exception.
                return new PaymentResult
                {
                    status = false,
                    Massage = $"Stripe error: {ex.StripeError.Message}" // Provide the error message returned by Stripe.
                };
            }
        }
        public async Task<PaymentResult> Payment2(long total, int orderId, string stripeToken)
        {
            try
            {
                // Set the Stripe API key
                StripeConfiguration.ApiKey = configuration["Stripe:PublishableKey"];


                var paymentMethodOptions = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions
                    {
                        Token = stripeToken
                    }
                };

                var paymentMethodService = new PaymentMethodService();
                var paymentMethod = await paymentMethodService.CreateAsync(paymentMethodOptions);


                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    // ... other options
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                        AllowRedirects = "never"
                    },
                    Amount = total,
                    Currency = "egp",
                    PaymentMethod = paymentMethod.Id,
                    Metadata = new Dictionary<string, string>
                    {
                    { "orderId", orderId.ToString() }
                     }
               };

                // Create and confirm the PaymentIntent
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions);
                // Check the status before confirming
                if (paymentIntent.Status == "requires_confirmation")
                {
                    // Confirm the payment intent
                    paymentIntent = await paymentIntentService.ConfirmAsync(paymentIntent.Id);

                    // Check the PaymentIntent status
                    if (paymentIntent.Status == "succeeded")
                    {
                        // Update payment details in the database
                        var result = await UpdateStripePaymentId(orderId, paymentIntent.Id, paymentIntent.Id);
                        if (result == 0)
                        {
                            return new PaymentResult
                            {
                                status = false,
                                Massage = "Payment succeeded, but database update failed."
                            };
                        }

                        return new PaymentResult
                        {
                            status = true,
                            Massage = "Payment completed successfully",
                            session = paymentIntent.Id // Optionally return the PaymentIntent ID for tracking
                        };
                    }
                    if (paymentIntent.Status == "failed")
                    {
                        var errorMessage = paymentIntent.LastPaymentError?.Message ?? "Unknown error";
                        return new PaymentResult
                        {
                            status = false,
                            Massage = $"Payment failed. Error: {errorMessage}. Please try again."
                        };
                    }

                    else if (paymentIntent.Status == "requires_action")
                    {
                        return new PaymentResult
                        {
                            status = false,
                            Massage = "Payment requires additional action. Please follow up with the customer."
                        };
                    }
                    else
                    {
                        return new PaymentResult
                        {
                            status = false,
                            Massage = "Payment failed. Please try again."
                        };
                    }
                }

                else
                {
                    return new PaymentResult
                    {
                        status = false,
                        Massage = "Payment failed. Please try again."
                    };
                }
            }
            catch (StripeException ex)
            {
                return new PaymentResult
                {
                    status = false,
                    Massage = $"Stripe error: {ex.StripeError.Message}"
                };
            }
        }


    }
}
