using Archysoft.D1.Model.Services.Abstract;
using Archysoft.D1.Web.Api.Model;

namespace Archysoft.D1.Web.Api.Controllers
{
    public class ProfileController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public ApiResponse GetProfile()
        {
            return null;
        }
    }
}
