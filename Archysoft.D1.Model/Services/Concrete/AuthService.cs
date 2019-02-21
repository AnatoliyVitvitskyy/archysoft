using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Archysoft.D1.Data.Entities;
using Archysoft.D1.Data.Repositories.Abstract;
using Archysoft.D1.Model.Auth;
using Archysoft.D1.Model.Exceptions;
using Archysoft.D1.Model.Services.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace Archysoft.D1.Model.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISettingsService _settingsService;

        public AuthService(IUserRepository userRepository, ISettingsService settingsService)
        {
            _userRepository = userRepository;
            _settingsService = settingsService;
        }

        public TokenModel Login(LoginModel model)
        {
            var user = _userRepository.Get(model.Login, model.Password);
            if (user == null)
            {
                throw new BusinessException(-2, "Invalid User");
            }

            return GenerateToken(user);
        }

        private TokenModel GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.JwtSettings.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays);

            var jwtToken =  new JwtSecurityToken(
                _settingsService.JwtSettings.Issuer,
                null,
                claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresIn = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays)
            };
        }
    }
}
