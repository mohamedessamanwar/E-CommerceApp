using Business_Access_Layer.DTOS.ShoppingCart;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.AddressDtos;
using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.DTOS.ShoppingCart;
using BusinessAccessLayer.Services.AddressService;
using BusinessAccessLayer.Services.ProductService;
using BusinessAccessLayer.Services.ShoppingCartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_CommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IProductServices productServices;
        private readonly IDataProtector _protector;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IProductServices productServices , IDataProtectionProvider provider)
        {
            this.shoppingCartService = shoppingCartService;
            this.productServices = productServices;
            _protector = provider.CreateProtector("Sensitive.Data.Protection");
        }
         #region createCart
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateShoppingCart(ShoppingCartCreateView shoppingCartCreateView)
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

            int idp = shoppingCartCreateView.ProductId;
            var product = await productServices.ProductWithCategory(idp);
            if (product == null) { return NewResult(new ResponseHandler().NotFound<ShoppingCartCreateView>("Not Found Product!")); }
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            shoppingCartCreateView.UserId = userIdFromToken;  //"a877ffd6-71b9-4eae-8f70-3f02774fe1ae";
            var result = await shoppingCartService.CreateShoppingCart(shoppingCartCreateView);
            if (result.Status == false)
            {
                return NewResult(new ResponseHandler().BadRequest<ShoppingCartCreateView>(result.Massage));
            }
            return NewResult(new ResponseHandler().Created<ShoppingCartCreateView>(shoppingCartCreateView));
        }
        #endregion

         #region view Cart
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ViewShoppingCart()
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var Cart = await shoppingCartService.ViewShoppingCart(userIdFromToken);
            if (Cart == null) { return NewResult(new ResponseHandler().NotFound<ShoppingCartView>("Not Found Products!")); }
            return NewResult(new ResponseHandler().Success<ShoppingCartView>(Cart));
        }
        #endregion

        [HttpPost("increase/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> IncreaseCartCountByOne(string id )
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
            var _id = int.Parse(_protector.Unprotect(id));
            var result = await shoppingCartService.IncreaseCartCountByOne(_id);

            if (result.Status == false)
            {
                return NewResult(new ResponseHandler().BadRequest<CreateStatus<ShoppingCartCreateView>>(result.Massage));
            }

            return NewResult(new ResponseHandler().Success<CreateStatus<ShoppingCartCreateView>>(result));
        }

        [HttpPost("decrease/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DecreaseCartCountByOne(string id)
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
            var _id = int.Parse(_protector.Unprotect(id));
            var result = await shoppingCartService.decresedCartCountByOne(_id);

            if (result.Status == false)
            {
                return NewResult(new ResponseHandler().BadRequest<CreateStatus<ShoppingCartCreateView>>(result.Massage));
            }

            return NewResult(new ResponseHandler().Success<CreateStatus<ShoppingCartCreateView>>(result));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteCart(string id)
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
            var _id = int.Parse(_protector.Unprotect(id));
            var result = await shoppingCartService.Remove(_id);

            if (result.Status == false)
            {
                return NewResult(new ResponseHandler().BadRequest<CreateStatus<ShoppingCartCreateView>>(result.Massage));
            }

            return NewResult(new ResponseHandler().Success<CreateStatus<ShoppingCartCreateView>>(result));
        }


    }
}
