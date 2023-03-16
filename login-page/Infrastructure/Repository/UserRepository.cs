using login_page.Entities.User;
using login_page.Infrastructure.Data;
using login_page.Infrastructure.IRepository;

namespace login_page.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        
    }
}
