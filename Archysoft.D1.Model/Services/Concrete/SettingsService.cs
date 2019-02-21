using Archysoft.D1.Model.Services.Abstract;
using Archysoft.D1.Model.Settings;

namespace Archysoft.D1.Model.Services.Concrete
{
    public class SettingsService : ISettingsService
    {
        public JwtSettings JwtSettings { get; set; }

        public SettingsService(JwtSettings jwtSettings)
        {
            JwtSettings = jwtSettings;
        }
    }
}
