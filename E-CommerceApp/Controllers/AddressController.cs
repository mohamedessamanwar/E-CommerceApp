using BusinessAccessLayer.DTOS.AddressDtos;
using BusinessAccessLayer.DTOS.ProductDtos;
using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.Services.AddressService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Data.Models;

namespace E_CommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController
    {
        private readonly IAddressService addressService;
        private readonly UserManager<ApplicationUser> _Usermanager;
        public AddressController(IAddressService addressService, UserManager<ApplicationUser> usermanager)
        {
            this.addressService = addressService;
            _Usermanager = usermanager;
        }
        #region Add Adddress
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult> AddAddress([FromBody] CreateAddressDto createAddressDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList();
                return NewResult(new ResponseHandler().BadRequest<CreateAddressDto>(errors.ToString()));
            }
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            createAddressDto.UserId = userIdFromToken;

            var result = await addressService.CreateAddress(createAddressDto);
            if (result.Status == false)
            {
                return NewResult(new ResponseHandler().BadRequest<CreateAddressDto>(result.Massage));

            }
            return NewResult(new ResponseHandler().Created<CreateAddressDto>(createAddressDto));
        }
        #endregion
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAddressByUserId()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread ID before00 awaiting: {threadId}");
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var result = await  addressService.ViewAddressByUserId(userIdFromToken);

            if (result.Count() == 0)
            {
                return NewResult(new ResponseHandler().NotFound<ViewAddressDto>("Not Found Address"));
            }
            return NewResult(new ResponseHandler().Success<List<ViewAddressDto>>(result));

        }

    }
}
