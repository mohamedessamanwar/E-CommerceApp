using AutoMapper;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.OrderDtos;
using BusinessAccessLayer.Services.PaymentService;
using BusinessAccessLayer.Services.ShoppingCartService;
using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.UnitOfWorkRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IPayment paymentMethod;
        private readonly IMapper mapper;
        public OrderService(IUnitOfWork unitOfWork, IShoppingCartService shoppingCartService, IPayment payment, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.shoppingCartService = shoppingCartService;
            this.paymentMethod = payment;
            this.mapper = mapper;
        }
        #region add order
        public async Task<OrderAddState> AddOrder(OrderCreateDto orderCreateDto)
        {
            try
            {
                using (var transaction = await unitOfWork.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
                {
                    //add order record . 
                    var order = new DataAccessLayer.Data.Models.Order()
                    {
                        CreateAt = DateTime.Now,
                        OrderPaymentStatus = Helper.Helper.PaymentPending,
                        OrderStatus = Helper.Helper.OrderProcessing,
                        AddressId = orderCreateDto.AddressId,
                        UserId = orderCreateDto.UserId,
                        OrderPaymentType = orderCreateDto.OrderPaymentType,
                        OrderTotal = (double)(await unitOfWork.shoppiingCartRepo.GetShoppingWithProductView(orderCreateDto.UserId)).Sum(o => o.Count * o.Product.CurrentPrice)
                    };
                    await unitOfWork.orderRepo.AddAsync(order);
                    int orderresult = unitOfWork.Complete();
                    if (orderresult == 0)
                    {
                        transaction.Rollback();
                        return new OrderAddState
                        {
                            State = false,
                            Massage = "Somthing Wrong"
                        };
                    }
                    var carts = await unitOfWork.shoppiingCartRepo.GetShoppingWithProductView(orderCreateDto.UserId);
                    if (carts == null)
                    {
                        transaction.Rollback();
                        return new OrderAddState
                        {
                            State = false,
                            Massage = "Cart is empty"
                        };
                    }
                    // check item count  add order Items . 
                    foreach (var cart in carts)
                    {
                        // lock ...
                        var product = await unitOfWork.productRepository.GetProductWithLock(cart.ProductId);
                        if ( product == null|| product.Count + cart.Count < cart.Count )
                        {
                            transaction.Rollback();
                            return new OrderAddState
                            {
                                State = false,
                                Massage = $"count of product {product?.Name} is not enough"
                            };
                        }
                        var orderItem = new OrderDetail()
                        {
                            Price = (double)product.CurrentPrice,
                            Count = cart.Count,
                            OrderId = order.Id,
                            ProductId = product.Id
                        };
                        await unitOfWork.orderItemRepo.AddAsync(orderItem);
                        int orderItemResult = unitOfWork.Complete();
                        if (orderItemResult == 0)
                        {
                            transaction.Rollback();
                            return new OrderAddState
                            {
                                State = false,
                                Massage = "Somthing Wrong"
                            };
                        }
                    }
                    // end of cart ... 
                    // call payment ...

                    var payment = await paymentMethod.Payment((List<ShoppingCart>)carts, order.Id);
                    if (payment.status == false)
                    {
                        transaction.Rollback();
                        return new OrderAddState
                        {
                            State = false,
                            Massage = payment.Massage
                        };
                    }
                    transaction.Commit();
                    return new OrderAddState
                    {
                        Type = "Credit",
                        State = true,
                        Massage = payment.Massage,
                        SessionUrl = payment.session
                    };
                }
            } // try 
            catch (System.Exception ex)
            {
                return new OrderAddState
                {
                    State = false,
                    Massage = ex.Message
                };
            }

        } 
        #endregion
        public async Task<OrderPaymentStatus> OrderConfirmation(int orderId)
        {
            var order = await unitOfWork.orderRepo.GetByIdAsync(orderId);
            if (order == null)
            {
                return new OrderPaymentStatus(){
                    Status = false,
                    StatusMessage = "Order Not Found"

                };
            }
            var paymentStatus = paymentMethod.PaymentStatus(order.SessionId);
            var session = paymentMethod.PaymentSession(order.SessionId);
            await paymentMethod.UpdateStripePaymentId(orderId, session.Id, session.PaymentIntentId);
            if (paymentStatus == "paid")
            {               
                await UpdateOrderStatus(order.Id, Helper.Helper.PaymentPaid);
                await shoppingCartService.ClearCartAsync(order.UserId);
                 return new OrderPaymentStatus()
                {
                    Status = true,
                    StatusMessage = "Payment Done"

                }; ;
            }
            await UpdateOrderStatus(order.Id, "Canceled Payment");
           //  await shoppingCartService.ClearCartAsync(order.UserId);
            await paymentMethod.CancelPayment(session.PaymentIntentId);
            unitOfWork.orderRepo.Delete(order);
            unitOfWork.Complete();
            return new OrderPaymentStatus() { Status = false, StatusMessage = "Payment Is Not Complelte" };
        }
        private async Task<int> UpdateOrderStatus(int id, string paymentPaid)
        {
            var order = await unitOfWork.orderRepo.GetByIdAsync(id);          
            order.OrderPaymentStatus = paymentPaid;
            order.UpdateAt = DateTime.UtcNow;
            unitOfWork.orderRepo.Update(order, nameof(order.OrderPaymentStatus), nameof(order.UpdateAt));
            return  unitOfWork.Complete();
        }

        #region v1 
        //private async Task<PaymentResult>Payment(List<ShoppingCart> shoppingCart , int orderId)
        //{
        //    //stripe settings
        //    // Set the Stripe API key
        //    StripeConfiguration.ApiKey = "sk_test_51OzGU0J8wl246iKd7y4rBhbVNkLKf60cCrE4cmpBS3kuIdIqL9MBsJIU5ls2wGjbB2ZbF6pOTIKm9xnTUOFjFkn700NQmFMvOv"; // Replace with your actual API key
        //    var domain = "https://localhost:7138/";
        //    var options = new SessionCreateOptions
        //    {
        //        LineItems = new List<SessionLineItemOptions>(),

        //        Mode = "payment",
        //        SuccessUrl = domain + $"api/Order/OrderConfirmation?orderid={orderId}",
        //        CancelUrl = domain + $"api/Order/cancelorder",
        //    };

        //    foreach (var item in shoppingCart)
        //    {
        //        var sessionLineItem = new SessionLineItemOptions
        //        {
        //            PriceData = new SessionLineItemPriceDataOptions
        //            {
        //                UnitAmount = (long)(item.Product.CurrentPrice * 100),
        //                Currency = "try",
        //                ProductData = new SessionLineItemPriceDataProductDataOptions
        //                {
        //                    Name = item.Product.Name
        //                },
        //            },
        //            Quantity = item.Count,
        //        };
        //        options.LineItems.Add(sessionLineItem);
        //    }

        //    var service = new SessionService();
        //    Session session = service.Create(options);
        //    if (session == null)
        //    {
        //      return  new PaymentResult()
        //        {
        //            status = false,
        //            Massage = "wrong in complete payment"
        //        };
        //    }
        //    var result = await UpdateStripePaymentId(orderId, session.Id, session.PaymentIntentId);
        //    if (result == 0)
        //    {
        //        return new PaymentResult()
        //        {
        //            status = false,
        //            Massage = "wrong in complete payment"
        //        };
        //    }
        //    return new PaymentResult()
        //    {
        //        status = true,
        //        Massage = "complete payment" , 
        //        session = session.Url

        //    };
        //}
        //private string PaymentStatus(string sessionId)
        //{
        //    var service = new SessionService();
        //    Session session = service.Get(sessionId);
        //    return session.PaymentStatus.ToLower();
        //}
        //private Session PaymentSession(string sessionId)
        //{
        //    var service = new SessionService();
        //    Session session = service.Get(sessionId);
        //    return session;
        //}
        //private async Task<int> UpdateStripePaymentId(int orderId, string id, string paymentIntentId)
        //{
        //    var order = await unitOfWork.orderRepo.GetByIdAsync(orderId);
        //    if (order == null) { return 0; }
        //    order.PaymentIntentId = paymentIntentId;
        //    order.SessionId = id;
        //    order.UpdateAt = DateTime.UtcNow;
        //    unitOfWork.orderRepo.Update(order);
        //    var result = unitOfWork.Complete();
        //    return result;  
        //}
        //private async Task<PaymentResult> CancelPayment(string paymentIntentId)
        //{
        //    // Set the Stripe API key
        //    StripeConfiguration.ApiKey = "sk_test_51OzGU0J8wl246iKd7y4rBhbVNkLKf60cCrE4cmpBS3kuIdIqL9MBsJIU5ls2wGjbB2ZbF6pOTIKm9xnTUOFjFkn700NQmFMvOv"; // Replace with your actual API key

        //    var service = new PaymentIntentService();
        //    try
        //    {
        //        PaymentIntent paymentIntent = await service.CancelAsync(paymentIntentId);
        //        if (paymentIntent.Status == "canceled")
        //        {
        //            return new PaymentResult()
        //            {
        //                status = true,
        //                Massage = "Payment successfully canceled"
        //            };
        //        }
        //        else
        //        {
        //            return new PaymentResult()
        //            {
        //                status = false,
        //                Massage = "Failed to cancel the payment"
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new PaymentResult()
        //        {
        //            status = false,
        //            Massage = $"Error in canceling payment: {ex.Message}"
        //        };
        //    }
        //} 
        #endregion


    }
}
       

