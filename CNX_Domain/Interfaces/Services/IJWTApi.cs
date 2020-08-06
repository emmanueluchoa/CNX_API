using CNX_Domain.Models;

namespace CNX_Domain.Interfaces.Services
{
    public interface IJWTApi
    {
        AuthenticatedUserVM GenerateApiToken(string username, string userpass);
    }
}
