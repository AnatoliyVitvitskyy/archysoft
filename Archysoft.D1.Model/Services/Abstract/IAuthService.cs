using Archysoft.D1.Model.Auth;

namespace Archysoft.D1.Model.Services.Abstract
{
    public interface IAuthService
    {
        TokenModel Login(LoginModel model);
    }
}
