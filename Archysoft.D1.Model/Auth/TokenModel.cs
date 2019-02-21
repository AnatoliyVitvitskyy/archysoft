using System;

namespace Archysoft.D1.Model.Auth
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
