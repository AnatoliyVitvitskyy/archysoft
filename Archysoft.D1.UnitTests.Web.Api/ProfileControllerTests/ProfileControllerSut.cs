using Archysoft.D1.Model.Services.Abstract;
using Archysoft.D1.Web.Api.Controllers;
using Moq;

namespace Archysoft.D1.UnitTests.Web.Api.ProfileControllerTests
{
    public class ProfileControllerSut
    {
        public ProfileController Instance { get; set; }
        public Mock<IProfileService> ProfileService { get; set; }

        public ProfileControllerSut()
        {
            ProfileService = new Mock<IProfileService>();
            Instance = new ProfileController(ProfileService.Object);
        }
    }
}
