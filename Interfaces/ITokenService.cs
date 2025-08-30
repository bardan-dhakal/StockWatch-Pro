using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface ITokenService
    {
        CredentialTokenDto CreateToken(AppUser appUser);
    }
}