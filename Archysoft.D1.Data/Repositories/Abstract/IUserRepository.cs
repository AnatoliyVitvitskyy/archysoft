using System.Collections.Generic;
using Archysoft.D1.Data.Entities;

namespace Archysoft.D1.Data.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string email, string password);
    }
}
