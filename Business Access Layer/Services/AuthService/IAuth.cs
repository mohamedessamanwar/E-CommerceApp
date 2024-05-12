using Business_Access_Layer.DTOS.AuthDtos;
using BusinessAccessLayer.DTOS.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace Business_Access_Layer.Services.AuthService
{
    public interface IAuth
    {
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> ValidationCode(TokenRequestModelCode model);
        Task<AuthModel> ConfirmEmail([FromQuery] string userId, [FromQuery] string token);
        Task<AuthModel> ValidationCode(ConfirmCode confirmCode);
        Task<AuthModel> SendCodeForgetPassward(string Email);
        Task<AuthModel> Reset_Password(UserResetPasswordDto userResetPasswordDto);
    }
}
