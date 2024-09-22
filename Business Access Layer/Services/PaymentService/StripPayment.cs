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
                        Currency = "try",
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
            unitOfWork.orderRepo.Update(order);
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
    }
}
