using Archysoft.D1.Model.Common;
using Archysoft.D1.Model.Users;

namespace Archysoft.D1.Model.Services.Abstract
{
    public interface IUserService
    {
        SearchResult<UserGridModel> Get(BaseFilter filter);
    }
}
