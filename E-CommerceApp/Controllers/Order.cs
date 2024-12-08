using BusinessAccessLayer.DTOS.AddressDtos;
using BusinessAccessLayer.DTOS.OrderDtos;
using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.DTOS.ShoppingCart;
using BusinessAccessLayer.Services.AddressService;
using BusinessAccessLayer.Services.Email;
using BusinessAccessLayer.Services.OrderService;
using BusinessAccessLayer.Services.ShoppingCartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace E_CommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IMailingService mailingService;
        private readonly IAddressService addressService;
        public Order(IOrderService orderService, IMailingService mailingService, IAddressService addressService)
        {
            this.orderService = orderService;
            this.mailingService = mailingService;
            this.addressService = addressService;
        }
        #region V1
        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult> IndexAsync(OrderCreateDto orderCreateDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors)
        //                           .Select(e => e.ErrorMessage)
        //                           .ToList();
        //        return NewResult(new ResponseHandler().BadRequest<ShoppingCartCreateView>(string.Join(", ", errors)));
        //    }

        //    var address = await addressService.GetAddressByUserId(orderCreateDto.AddressId);
        //    if (address == null)
        //    {
        //        return NewResult(new ResponseHandler().NotFound<string>("Address Not Found"));
        //    }
        //    var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        //    // orderCreateDto.UserId = userIdFromToken;  
        //    var result = await orderService.AddOrder(orderCreateDto,userIdFromToken);
        //    if (result.State == false)
        //    {
        //        return NewResult(new ResponseHandler().BadRequest<OrderCreateDto>(result.Massage));
        //    }
        //    return NewResult(new ResponseHandler().Success<OrderAddState>(result));

        //} 
        #endregion
        [HttpPost]
        [Route("AddOrder")]
        [Authorize]
        
        public async Task<ActionResult> AddOrder(OrderCreateDto orderCreateDto)
        {
            var address = await addressService.GetAddressByUserId(orderCreateDto.AddressId);
            if (address == null)
            {
                return NewResult(new ResponseHandler().NotFound<string>("Address Not Found"));
            }
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            // orderCreateDto.UserId = userIdFromToken;  
            var result = await orderService.AddOrder(orderCreateDto, userIdFromToken);
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

        // AES decryption method
        private string Decrypt(string encryptedData)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("hjghjghgjhgjhgjhgjhgjhgjhghbjhhbh");
                aesAlg.IV = new byte[16]; // Initialization Vector (using a zero vector for simplicity)

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}

