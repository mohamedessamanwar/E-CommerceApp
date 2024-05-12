using Business_Access_Layer.DTOS.AuthDtos;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.AuthDtos;
using BusinessAccessLayer.Services.Email;
using DataAccessLayer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Business_Access_Layer.Services.AuthService
{
    public class AuthService : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IMailingService _mailingService;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, IMailingService mailingService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _mailingService = mailingService;
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            ApplicationUser? userCheack = await _userManager.FindByEmailAsync(model.Email);
            if (userCheack is not null)
            {
                return new AuthModel { Message = "Email is aready exixt" };
            }
            userCheack = await _userManager.FindByNameAsync(model.Username);
            if (userCheack is not null)
            {
                return new AuthModel { Message = "UserName is aready exixt" };
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var Result = await _userManager.CreateAsync(user, model.Password);
            if (!Result.Succeeded)
            {
                string errors = string.Empty;
                // StringBuilder errors = new StringBuilder();
                foreach (var error in Result.Errors)
                {
                    // error += e.Description;
                    errors += $"{error.Description},";
                    //errors.Append($"{error.Description},");
                    return new AuthModel { Message = errors };
                }
            }
            // Add to role 
            await _userManager.AddToRoleAsync(user, "User");
            var role = await AddClaimsAsync(user);
            if (role is not null)
            {
                return new AuthModel { Message = role };
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // code value have to be Encoded before sending the call.
            // string codeHtmlVersion = HttpUtility.UrlEncode(token);
            //HttpUtility.UrlEncode converted all plus signs (+) to empty spaces (" ") this is wrong,
            //UrlEncode replaces "+" to
            //"%2b". If you use + with UrlDecode, it will be replaced into whitespace character
            string codeHtmlVersion = HttpUtility.UrlEncode(token.Replace("+", "%2b"));

            var confirmationLink = $"https://localhost:44367/api/Auth/confirm-email?userId={user.Id}&token={codeHtmlVersion}";
            await _mailingService.SendEmailAsync(model.Email, "Confirm your email",
                $"Please confirm your email address by clicking <a href='{confirmationLink}'>here</a>.");
            return new AuthModel
            {
                Message = confirmationLink,
                IsAuthenticated = true
            };
        }
        public async Task<AuthModel> ConfirmEmail(string userId, string token)
        {
            var decodedCode = HttpUtility.UrlDecode(token);
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new AuthModel { Message = "Email is not valid" };
            }
            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);
            if (!result.Succeeded)
            {
                return new AuthModel { Message = "Token Is Not Valid", IsAuthenticated = false };
            }
            return new AuthModel { Message = "Login Now", IsAuthenticated = true };

        }
        private async Task<string> AddClaimsAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id) ,

            }
            .Union(roleClaims);
            var result = await _userManager.AddClaimsAsync(user, claims);
            string Error = null;
            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                {
                    Error += $"{error.Description},";
                }
            }
            return Error;

        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private string GenerateKey(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            // Get Code .
            string Code = GenerateKey(6);
            // Save Code .
            user.Code = Code;
            await _userManager.UpdateAsync(user);
            //send email .
            await _mailingService.SendEmailAsync(model.Email, "Your Code", Code);
            authModel.IsAuthenticated = true;
            authModel.Email = model.Email;
            return authModel;
        }
        public async Task<AuthModel> ValidationCode(TokenRequestModelCode model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            AuthModel authModel = new AuthModel();
            if (user is null)
            {
                authModel.Message = "Email is incorrect!";
                return authModel;
            }
            if (user.Code != model.Code)
            {
                authModel.Message = "Code Is Not Valid";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };


        }
        public async Task<AuthModel> SendCodeForgetPassward(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
            {
                return new AuthModel { IsAuthenticated = false, Message = "Email is not found" };
            }
            string Code = GenerateKey(6);
            user.Code = Code;
            await _userManager.UpdateAsync(user);
            await _mailingService.SendEmailAsync(Email, "Your Code", Code);
            return new AuthModel { IsAuthenticated = true, Message = "Code is Send" }; ;

        }
        public async Task<AuthModel> ValidationCode(ConfirmCode confirmCode)
        {
            var user = await _userManager.FindByEmailAsync(confirmCode.Email);
            if (user is null)
            {
                return new AuthModel { IsAuthenticated = false, Message = "User is not found" };
            }
            if (confirmCode.Code != user.Code)
            {
                return new AuthModel { IsAuthenticated = false, Message = "Code is not Correct" };
            }
            return new AuthModel { IsAuthenticated = true, Message = "Code is correct" };
        }
        public async Task<AuthModel> Reset_Password(UserResetPasswordDto userResetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(userResetPasswordDto.Email);
            if (user is null)
            {
                return new AuthModel { IsAuthenticated = false, Message = "User is not found" };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var Result = await _userManager.ResetPasswordAsync(user, token, userResetPasswordDto.NewPassword);
            if (!Result.Succeeded)
            {
                string errors = string.Empty;
                // StringBuilder errors = new StringBuilder();
                foreach (var error in Result.Errors)
                {
                    // error += e.Description;
                    errors += $"{error.Description},";
                    //errors.Append($"{error.Description},");
                    return new AuthModel { Message = errors };
                }
            }
            return new AuthModel { IsAuthenticated = true, Message = "Password is Reset" };
        }
    }
}
