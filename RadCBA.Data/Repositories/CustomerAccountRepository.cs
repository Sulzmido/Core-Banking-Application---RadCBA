using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Data.Repositories
{
    public class CustomerAccountRepository : BaseRepository<CustomerAccount>
    {
        private AppContext db = new AppContext();

        public bool AnyAccountOfType(AccountType type)
        {
            //using (ISession session = NHibernateHelper.OpenSession())
            //{
            //    return session.Query<CustomerAccount>().Any(a => a.AccountType == type);
            //}
            return db.CustomerAccounts.Any(a => a.AccountType == type);
        }
        public CustomerAccount GetByAccountNumber(long actNo)
        {
            //using (ISession session = NHibernateHelper.OpenSession())
            //{
            //    return session.Query<CustomerAccount>().Where(a => a.AccountNumber == actNo).SingleOrDefault();
            //}
            return db.CustomerAccounts.Where(a => a.AccountNumber == actNo).SingleOrDefault();

        }
        public List<CustomerAccount> GetByType(AccountType actType)
        {
            //using (ISession session = NHibernateHelper.OpenSession())
            //{
            //    return session.Query<CustomerAccount>().Where(a => a.AccountType == actType).ToList();
            //}
            return db.CustomerAccounts.Where(a => a.AccountType == actType).ToList();
        }

        public int GetCountByCustomerActType(AccountType actType, int customerId)
        {
            return db.CustomerAccounts.Where(a => a.AccountType == actType && a.Customer.ID == customerId).Count();
        }
    }
}
