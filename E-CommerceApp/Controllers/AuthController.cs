using Business_Access_Layer.DTOS.AuthDtos;
using BusinessAccessLayer.Services.AuthService;
using BusinessAccessLayer.DTOS.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                // Get all the errors in ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(errors);
            }
            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { massage = result });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("CheackCode")]
        public async Task<IActionResult> Code([FromBody] TokenRequestModelCode model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ValidationCode(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _authService.ConfirmEmail(userId, token);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { massage = result });
        }

        [HttpPost("Send-Code")]
        public async Task<IActionResult> SendCode(EmailConfirmation emailConfirmation)
        {
            var result = await _authService.SendCodeForgetPassward(emailConfirmation.Email);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(new { massage = result });
        }
        [HttpPost("confirm-Code")]
        public async Task<IActionResult> ValidationCode(ConfirmCode confirmCode)
        {
            var result = await _authService.ValidationCode(confirmCode);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { massage = result });
        }
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            var result = await _authService.Reset_Password(userResetPasswordDto);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { massage = result });
        }

    }
}
