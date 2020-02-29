using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private AppContext db = new AppContext();

        public Role GetByName(string name)
        {
            return db.Roles.Where(r => r.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
        }
    }
}
