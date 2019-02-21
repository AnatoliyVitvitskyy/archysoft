using Archysoft.D1.Model.Settings;

namespace Archysoft.D1.Model.Services.Abstract
{
    public interface ISettingsService
    {
        JwtSettings JwtSettings { get; set; }
    }
}
