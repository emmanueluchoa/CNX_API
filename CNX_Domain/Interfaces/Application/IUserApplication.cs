using CNX_Domain.Models;
using System.Collections.Generic;

namespace CNX_Domain.Interfaces.Application
{
    public interface IUserApplication
    {
        IList<UserVM> GetUsers();
        UserVM GetById(string id);
        UserVM CreateUser(UserVM userVM);
        UserVM UpdateUser(UserVM userVM);
        UserVM GetUser(string userName, string userPassword);
        bool ValidateIfUserExists(string userName, string userPassword);
    }
}
