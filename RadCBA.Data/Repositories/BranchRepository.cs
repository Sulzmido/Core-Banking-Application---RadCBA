using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Data.Repositories
{
    public class BranchRepository : BaseRepository<Branch>
    {
        //public Branch GetByName(string name)
        //{
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        return session.Query<Branch>().Where(b => b.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
        //    }
        //}

        // dependency injection and disposing db 

        private AppContext db = new AppContext();

        public Branch GetByName(string name)
        {
            return db.Branches.Where(b => b.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
        }

        public List<Branch> GetAll()
        {
            return db.Branches.ToList();
        }

    }
}
