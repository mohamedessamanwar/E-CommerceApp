using BusinessAccessLayer.Services.AuthService;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.AuthDtos;
using BusinessAccessLayer.Services.Email;
using DataAccessLayer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace UnitTest
{

    [TestFixture]
    public class LoginUnitTest
    {
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private Mock<IOptions<JWT>> _jwtOptionsMock;
        private Mock<IMailingService> _mailingServiceMock;
        private AuthService _authService;
        private TokenRequestModel _tokenRequestModel;

        [SetUp]
        public void Setup()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null, null, null, null);

            _jwtOptionsMock = new Mock<IOptions<JWT>>();
            _jwtOptionsMock.Setup(x => x.Value).Returns(new JWT()); // Configure as needed

            _mailingServiceMock = new Mock<IMailingService>();

            _authService = new AuthService(_userManagerMock.Object, _roleManagerMock.Object, _jwtOptionsMock.Object, _mailingServiceMock.Object);

            _tokenRequestModel = new TokenRequestModel
            {
                Email = "test@example.com",
                Password = "Password123!"
            };
        }


        [Test]
        public async Task GetTokenAsync_InvalidEmail_ReturnsErrorMessage()
        {
            /// _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            var result = await _authService.GetTokenAsync(_tokenRequestModel);

            Assert.That("Email or Password is incorrect!", Is.EqualTo(result.Message));
        }

    }
}
