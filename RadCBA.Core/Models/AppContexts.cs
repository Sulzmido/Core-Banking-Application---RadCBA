using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Core.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("name=DefaultConnection")//base("name=NewContext")
        {

        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }

        public DbSet<GlCategory> GlCategories { get; set; }

        public System.Data.Entity.DbSet<RadCBA.Core.Models.GlAccount> GlAccounts { get; set; }

        public System.Data.Entity.DbSet<RadCBA.Core.Models.TillToUser> TillToUsers { get; set; }

        public System.Data.Entity.DbSet<RadCBA.Core.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<RadCBA.Core.Models.AccountConfiguration> AccountConfigurations { get; set; }

        public System.Data.Entity.DbSet<RadCBA.Core.Models.CustomerAccount> CustomerAccounts { get; set; }

        public System.Data.Entity.DbSet<RadCBA.Core.Models.GlPosting> GlPostings { get; set; }

        public System.Data.Entity.DbSet<RadCBA.Core.Models.TellerPosting> TellerPostings { get; set; }

        public DbSet<ExpenseIncomeEntry> ExpenseIncomeEntries { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }

    public class NewDbContext : DbContext
    {
        public NewDbContext() : base("name=NewContext")
        {

        }
        public DbSet<Crush> Crushes { get; set; }
        public DbSet<Rejecter> Rejecters { get; set; }
    }
}
