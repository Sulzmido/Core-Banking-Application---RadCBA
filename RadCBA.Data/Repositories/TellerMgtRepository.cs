using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Data.Repositories
{
    public class TellerMgtRepository : BaseRepository<TillToUser>
    {
        private AppContext db = new AppContext();
        
        public List<TillToUser> GetAll()
        {
            return db.TillToUsers.ToList();
        }
    }
}
