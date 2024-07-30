using BusinessAccessLayer.DTOS.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services.OrderService
{
    public interface IOrderService 
    {
        Task<OrderAddState> AddOrder(OrderCreateDto orderCreateDto);
        Task<OrderPaymentStatus> OrderConfirmation(int orderId);
    }
}
