using BusinessAccessLayer.DTOS;
using DataAccessLayer.Data.Models;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services.PaymentService
{
    public interface IPayment
    {
        Task<PaymentResult> Payment(List<ShoppingCart> shoppingCart, int orderId);
        Session PaymentSession(string sessionId);
        string PaymentStatus(string sessionId);
        Task<int> UpdateStripePaymentId(int orderId, string id, string paymentIntentId);

        Task<PaymentResult> CancelPayment(string paymentIntentId);
        Task<PaymentResult> Payment1(long total, int orderId, CardPaymentRequest cardPaymentRequest);
        Task<PaymentResult> Payment2(long total, int orderId, string stripeToken); 
    }
}
