using login_page.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_page.Infrastructure.IRepository
{
    public interface IUserRepository : IGenericRepository<User> 
    {
    }
}
