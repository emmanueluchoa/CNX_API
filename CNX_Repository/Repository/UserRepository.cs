using CNX_Domain.Entities;
using CNX_Domain.Interfaces.Repository;
using CNX_Repository.Context;

namespace CNX_Repository.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CnxContext context) : base(context) { }
    }
}
