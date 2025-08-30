using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly IConfiguration configuration = configuration;
        private readonly SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(GetSignInKey(configuration)));

        public CredentialTokenDto CreateToken(AppUser appUser)
        {
            List<Claim> claims = CreateClaims(appUser);

            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(claims, creds);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new CredentialTokenDto{
                UserName = appUser.UserName,
                Email = appUser.Email,
                Token = tokenHandler.WriteToken(token), 
            };
        }

        private static string GetSignInKey(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWT");

            return jwtSettings["SigningKey"] ?? throw new Exception();
        }

        private static List<Claim> CreateClaims(AppUser appUser)
        {
            return [
                new(JwtRegisteredClaimNames.Email,appUser.Email!),
                new(ClaimTypes.GivenName,appUser.UserName!)
            ];
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(List<Claim> claims, SigningCredentials creds)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds,
                Issuer = configuration[GetJwtIssuer()],
                Audience = configuration[GetJwtAudience()]
            };
        }

        private static string GetJwtIssuer()
        {
            return "JWT:Issuer";
        }

        private static string GetJwtAudience()
        {
            return "JWT:Audience";
        }
    }
}