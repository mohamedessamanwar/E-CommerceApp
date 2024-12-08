using Business_Access_Layer.DTOS.AuthDtos;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.AuthDtos;
using BusinessAccessLayer.Services.Email;
using DataAccessLayer.Data.Models;
using DataAccessLayer.UnitOfWorkRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace BusinessAccessLayer.Services.AuthService
{
    public class AuthService : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWT _jwt;
        private readonly IMailingService _mailingService;
        private readonly IUnitOfWork unitOfWork ;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt = null, IMailingService mailingService = null, IUnitOfWork unitOfWork = null, SignInManager<ApplicationUser> signInManager = null)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _mailingService = mailingService;
            this.unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }
        
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            // Check if the email already exists
            var userCheck = await _userManager.FindByEmailAsync(model.Email);
            if (userCheck is not null)
            {
                return new AuthModel { Message = "Email already exists" };
            }

            // Check if the username already exists
            userCheck = await _userManager.FindByNameAsync(model.Username);
            if (userCheck is not null)
            {
                return new AuthModel { Message = "Username already exists" };
            }

            // Create a new ApplicationUser
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                // Collect all error messages
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            // Assign the user to the "User" role
            await _userManager.AddToRoleAsync(user, "User");

            // Add claims to the user
            var role = await AddClaimsAsync(user);
            if (role is not null)
            {
                return new AuthModel { Message = role };
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await unitOfWork.userToken.AddAsync(new UserToken()
            {
                Token = token,
                UserEmail = user.Email,
                ApplicationUserId = user.Id,

            }); 
            unitOfWork.Complete();
            string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var confirmationLink = $"https://localhost:7138/api/Auth/confirm-email?userId={user.Id}&token={decodedToken}";

            // Send confirmation email
            await _mailingService.SendEmailAsync(model.Email, "Confirm your email",
                $"Please confirm your email address by clicking <a href='{confirmationLink}'>here</a>.");

            return new AuthModel
            {
                Message = "Registration successful. Please check your email to confirm your account.",
                IsAuthenticated = true
            };
        }

        public async Task<AuthModel> ConfirmEmail(string userId, string token)
        {
            // Decode the token received from the URL
         //   string decodedToken = HttpUtility.UrlDecode(token);

            // Retrieve the token from the database
            var userToken = await unitOfWork.userToken.GetUserToken(token);
            if (userToken == null || userToken.ApplicationUserId != userId)
            {
                return new AuthModel { Message = "Token is not valid", IsAuthenticated = false };
            }

            // Find the user and confirm their email
            var user = await _userManager.FindByIdAsync(userToken.ApplicationUserId);
            if (user == null)
            {
                return new AuthModel { Message = "User not found", IsAuthenticated = false };
            }

            var result = await _userManager.ConfirmEmailAsync(user, userToken.Token);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = $"Token is not valid: {errors}", IsAuthenticated = false };
            }

            user.EmailConfirmed = true;
            unitOfWork.Complete();

            return new AuthModel { Message = "Email confirmed successfully. You can now log in.", IsAuthenticated = true };
        }

        private async Task<string> AddClaimsAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

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
            if (user is null )
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            if (user.LockoutEnd>DateTimeOffset.UtcNow)
            {
                authModel.Message = "you are locked!";
                return authModel;

            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password,false, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            if (user.EmailConfirmed == false)
            {
                // Generate a new email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Store the original token in the database (not URL-encoded)
                await unitOfWork.userToken.AddAsync(new UserToken()
                {
                    Token = token,
                    UserEmail = user.Email,
                    ApplicationUserId = user.Id,
                });
                unitOfWork.Complete();

                // URL-encode the token for the confirmation link
                var encodedToken = HttpUtility.UrlEncode(token);
                var confirmationLink = $"https://localhost:7138/api/Auth/confirm-email?userId={user.Id}&token={encodedToken}";

                // Send the confirmation email
                await _mailingService.SendEmailAsync(model.Email, "Confirm your email",
                    $"Please confirm your email address by clicking <a href='{confirmationLink}'>here</a>.");

                authModel.Message = "Email is not confirmed. We have sent a confirmation email to your inbox.";
                return authModel;
            }
            string Code = GenerateKey(6);
            user.Code = Code;
            await _userManager.UpdateAsync(user);
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
        #region SendCodeForgetPasswardV1
        //public async Task<AuthModel> SendCodeForgetPassward(string Email)
        //{
        //    var user = await _userManager.FindByEmailAsync(Email);
        //    if (user is null)
        //    {
        //        return new AuthModel { IsAuthenticated = false, Message = "Email is not found" };
        //    }
        //    string Code = GenerateKey(6);
        //    user.Code = Code;
        //    await _userManager.UpdateAsync(user);
        //    await _mailingService.SendEmailAsync(Email, "Your Code", Code);
        //    return new AuthModel { IsAuthenticated = true, Message = "Code is Send" }; ;

        //} 
        #endregion

        public async Task<AuthModel> SendCodeForgetPassward(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
            {
                return new AuthModel { IsAuthenticated = false, Message = "Email is not found" };
            }
            var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var Code = $"https://localhost:44367/api/Auth/confirm-Code?token={Token}";
            var userToken = new UserToken()
            {
                UserEmail = Email,
                ApplicationUserId = user.Id,
                Token = Token,
                CreateAt = DateTime.UtcNow
            };
            await unitOfWork.userToken.AddAsync(userToken);
            unitOfWork.Complete();
            await _mailingService.SendEmailAsync(Email, "Your Code", Code);
            return new AuthModel { IsAuthenticated = true, Message = "Code is Send" }; 

          }
        #region ValidationCodeV1
        //    public async Task<AuthModel> ValidationCode(ConfirmCode confirmCode)
        //{
        //    var user = await _userManager.FindByEmailAsync(confirmCode.Email);
        //    if (user is null)
        //    {
        //        return new AuthModel { IsAuthenticated = false, Message = "User is not found" };
        //    }
        //    if (confirmCode.Code != user.Code)
        //    {
        //        return new AuthModel { IsAuthenticated = false, Message = "Code is not Correct" };
        //    }
        //    return new AuthModel { IsAuthenticated = true, Message = "Code is correct" };
        //} 
        #endregion
        public async Task<AuthModel> ValidationCode(string token)
        {
            var user = await unitOfWork.userToken.GetUserToken(token);
            if (user is null)
            {
                return new AuthModel { IsAuthenticated = false, Message = "Token is not found" };
            }
            return new AuthModel { IsAuthenticated = true, Message = "Token is correct" };
        }
        #region Reset_Password
        //public async Task<AuthModel> Reset_Password(UserResetPasswordDto userResetPasswordDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(userResetPasswordDto.Email);
        //    if (user is null)
        //    {
        //        return new AuthModel { IsAuthenticated = false, Message = "User is not found" };
        //    }
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var Result = await _userManager.ResetPasswordAsync(user, token, userResetPasswordDto.NewPassword);
        //    if (!Result.Succeeded)
        //    {
        //        string errors = string.Empty;
        //        // StringBuilder errors = new StringBuilder();
        //        foreach (var error in Result.Errors)
        //        {
        //            // error += e.Description;
        //            errors += $"{error.Description},";
        //            //errors.Append($"{error.Description},");
        //            return new AuthModel { Message = errors };
        //        }
        //    }
        //    return new AuthModel { IsAuthenticated = true, Message = "Password is Reset" };
        //} 
        #endregion
        public async Task<AuthModel> Reset_Password(UserResetPasswordDto userResetPasswordDto)
        {
            var token = await unitOfWork.userToken.GetUserToken(userResetPasswordDto.Token);
            if (token is null)
            {
                return new AuthModel { IsAuthenticated = false, Message = "Token is not found" };
            }
            ApplicationUser? user = await _userManager.FindByIdAsync(token.ApplicationUserId);
            var Result = await _userManager.ResetPasswordAsync(user, token.Token, userResetPasswordDto.NewPassword);
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
            unitOfWork.userToken.Delete(token);
            unitOfWork.Complete();
            return new AuthModel { IsAuthenticated = true, Message = "Password is Reset" };
        }
    }
}
