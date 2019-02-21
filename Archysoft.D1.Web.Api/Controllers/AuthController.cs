using Archysoft.D1.Model.Auth;
using Archysoft.D1.Model.Services.Abstract;
using Archysoft.D1.Web.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archysoft.D1.Web.Api.Controllers
{
    public class AuthController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(Routes.Login)]
        public ApiResponse<TokenModel> Login([FromBody] LoginModel model)
        {
            TokenModel token = _authService.Login(model);
            return new ApiResponse<TokenModel>(token);
        }
    }
}
