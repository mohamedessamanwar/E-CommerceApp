using BusinessAccessLayer.DTOS.AddressDtos;
using BusinessAccessLayer.DTOS.OrderDtos;
using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.DTOS.ShoppingCart;
using BusinessAccessLayer.Services.Email;
using BusinessAccessLayer.Services.OrderService;
using BusinessAccessLayer.Services.ShoppingCartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IMailingService mailingService;
        public Order(IOrderService orderService, IMailingService mailingService)
        {
            this.orderService = orderService;
            this.mailingService = mailingService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> IndexAsync(OrderCreateDto orderCreateDto)
        {
            if (!ModelState.IsValid)
            {
                // Extract the error messages
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();

                // Return the errors as a bad request response
                return NewResult(new ResponseHandler().BadRequest<ShoppingCartCreateView>(string.Join(", ", errors)));
            }
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            orderCreateDto.UserId = userIdFromToken;  //"a877ffd6-71b9-4eae-8f70-3f02774fe1ae";
            var result = await orderService.AddOrder(orderCreateDto);
            if (result.State == false)
            {
                return NewResult(new ResponseHandler().BadRequest<OrderCreateDto>(result.Massage));
            }
            return NewResult(new ResponseHandler().Success<OrderAddState>(result)); 

        }

        [HttpPost]
        [Route("OrderConfirmation")]
        public async Task<ActionResult> OrderConfirmation([FromQuery]int orderId)
        {
            var result = await orderService.OrderConfirmation(orderId);
            if (result.Status == false) {
               return NewResult(new ResponseHandler().BadRequest<OrderPaymentStatus>(result.StatusMessage));
            }         
            return NewResult(new ResponseHandler().Success<string>(result.StatusMessage)); ;
        }
        [HttpPost]
        [Route("cancelorder/{orderId}")]
        public async Task<ActionResult> CancelOrder(int orderId)
        {
                // return NewResult(new ResponseHandler().BadRequest<bool>("Error in Payment"));
                return BadRequest("error in payment");
        }

    }
}

