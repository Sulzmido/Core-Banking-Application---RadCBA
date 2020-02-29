using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        private AppContext db = new AppContext();
        public Customer GetById(string custId)
        {
            //using (ISession session = NHibernateHelper.OpenSession())
            //{
            //    return session.Query<Customer>().Where(c => c.CustId.ToLower().Equals(custId.ToLower())).FirstOrDefault();
            //}
            return db.Customers.Where(c => c.CustId.ToLower().Equals(custId.ToLower())).FirstOrDefault();
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }
    }
}
