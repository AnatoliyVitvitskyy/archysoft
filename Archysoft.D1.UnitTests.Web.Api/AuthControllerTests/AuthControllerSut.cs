using Archysoft.D1.Model.Services.Abstract;
using Archysoft.D1.Web.Api.Controllers;
using Moq;

namespace Archysoft.D1.UnitTests.Web.Api.AuthControllerTests
{
    public class AuthControllerSut
    {
        public AuthController Instance { get; set; }
        public Mock<IAuthService> AuthService { get; set; }

        public AuthControllerSut()
        {
            AuthService = new Mock<IAuthService>();
            Instance = new AuthController(AuthService.Object);
        }
    }
}
