using login_page.Entities.Shops;
using login_page.Infrastructure.Data;
using login_page.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_page.Infrastructure.Repository
{
    public class Storerepository : GenericRepository<Shop>, IStorerepository
    {
        public Storerepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
