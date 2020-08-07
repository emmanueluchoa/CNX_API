using CNX_Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNX_Domain.Interfaces.Application
{
    public interface IUserApplication
    {
        IList<UserVM> GetUsers();
        UserVM GetById(string id);
        Task<UserVM> CreateUser(CreateUserVM userVM);
        UserVM UpdateUser(UserVM userVM);
        Task<UserVM> GetUser(string userName, string userPassword);
        bool ValidateIfUserExists(string userName, string userPassword);
        Task ResetPasswordAsync(ResetPasswordUserVM resetPasswordUserVM);
    }
}
