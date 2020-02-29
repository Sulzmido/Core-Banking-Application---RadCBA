using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Data.Repositories
{
    public class GlCategoryRepo : BaseRepository<GlCategory>
    {
        private AppContext db = new AppContext();
        public GlCategory GetByName(string name)
        {
            return db.GlCategories.Where(c => c.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
        }
    }
}
