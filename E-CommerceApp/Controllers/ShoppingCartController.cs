using Business_Access_Layer.DTOS.ShoppingCart;
using BusinessAccessLayer.DTOS.AddressDtos;
using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.DTOS.ShoppingCart;
using BusinessAccessLayer.Services.AddressService;
using BusinessAccessLayer.Services.ProductService;
using BusinessAccessLayer.Services.ShoppingCartService;
using Microsoft.AspNetCore.Authorization;
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
        public ShoppingCartController(IShoppingCartService shoppingCartService, IProductServices productServices)
        {
            this.shoppingCartService = shoppingCartService;
            this.productServices = productServices;
        }
         #region createCart
        [HttpOptions]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> ViewShoppingCart()
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var Cart = await shoppingCartService.ViewShoppingCart(userIdFromToken);
            if (Cart == null) { return NewResult(new ResponseHandler().NotFound<ShoppingCartView>("Not Found Products!")); }
            return NewResult(new ResponseHandler().Success<ShoppingCartView>(Cart));
        } 
        #endregion
    }
}
