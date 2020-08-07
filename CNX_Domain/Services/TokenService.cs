using CNX_Domain.Entities;
using CNX_Domain.Interfaces.Application;
using CNX_Domain.Interfaces.Services;
using CNX_Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CNX_Domain.Services
{
    public class TokenService : IJWTApi
    {
        private readonly string _key = "847d66636d5512429164f7dedae86d41a2e9a5797dab2242d37b4193ae89ca20";
        private IUserApplication _userApplication;

        public TokenService(IUserApplication userApplication)
        {
            this._userApplication = userApplication;
        }

        public async Task<AuthenticatedUserVM> GenerateApiToken(string username, string userpass)
        {
            UserVM user = await this._userApplication.GetUser(username, userpass);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor securityTokenDescriptor = GenerateSecurityTokenDescriptor(user);

            AuthenticatedUserVM authenticatedUser = new AuthenticatedUserVM { Token = GenerateToken(jwtSecurityTokenHandler, securityTokenDescriptor), User = user };

            return authenticatedUser;
        }

        private string GenerateToken(JwtSecurityTokenHandler jwtSecurityTokenHandler, SecurityTokenDescriptor securityTokenDescriptor)
        {
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private SecurityTokenDescriptor GenerateSecurityTokenDescriptor(UserVM user)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Locality, user.Locale),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = GenerateSigningCredentials()
            };
        }

        private SigningCredentials GenerateSigningCredentials() =>
            new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)), SecurityAlgorithms.HmacSha256Signature);
    }
}
