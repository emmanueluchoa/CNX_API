using CNX_Domain.Models;
using System.Threading.Tasks;

namespace CNX_Domain.Interfaces.Services
{
    public interface IJWTApi
    {
        Task<AuthenticatedUserVM> GenerateApiToken(string username, string userpass);
    }
}
