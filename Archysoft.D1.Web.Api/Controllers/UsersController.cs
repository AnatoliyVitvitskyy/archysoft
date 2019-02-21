using Archysoft.D1.Model.Common;
using Archysoft.D1.Model.Services.Abstract;
using Archysoft.D1.Model.Users;
using Archysoft.D1.Web.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Archysoft.D1.Web.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route(Routes.Users)]
        public ApiResponse<SearchResult<UserGridModel>> GetUsers(BaseFilter filter)
        {
            SearchResult<UserGridModel>  users = _userService.Get(filter);
            return new ApiResponse<SearchResult<UserGridModel>>(users);
        }
    }
}
