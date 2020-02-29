using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Data.Repositories
{
    public class UserRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private AppContext db2 = new AppContext();

        public ApplicationUser GetByUsername(string username)
        {
            return db.Users.Where(u => u.UserName.ToLower().Equals(username.ToLower())).FirstOrDefault();
        }

        public ApplicationUser GetByEmail(string email)
        {
            return db.Users.Where(u => u.Email.ToLower().Equals(email.ToLower())).FirstOrDefault();
        }

        public ApplicationUser FindUser(string username, string passwordHash)
        {
            return db.Users.Where(u => u.UserName.ToLower().Equals(username.ToLower()) && u.PasswordHash.ToLower().Equals(passwordHash.ToLower())).FirstOrDefault();
        }

        public List<ApplicationUser> GetAllTellers()
        {
            //return db.Users.Where(u => u.Role.ID == 2 && u.EmailConfirmed == true).ToList();   //teller role has id of 2
            // get all application users that have claim to tellerposting
            return db.Users.Where(u => u.Role.RoleClaims.Any(r => r.Name.Equals("TellerPosting"))).ToList();       
        }

        public List<ApplicationUser> TellersWithoutTill()
        {
            var tellers = GetAllTellers();
            var output = new List<ApplicationUser>();

            var tillToUsers = db2.TillToUsers.ToList();
            foreach(var teller in tellers)
            {
                if(!tillToUsers.Any(tu => tu.UserId == teller.Id))
                {
                    output.Add(teller);
                }
            }
            return output;
        }

        public List<ApplicationUser> TellersWithTill()
        {
            var tellers = GetAllTellers();
            var output = new List<ApplicationUser>();
            var tillToUsers = db2.TillToUsers.ToList();
            foreach(var teller in tellers)
            {
                if(tillToUsers.Any(tu => tu.UserId == teller.Id))
                {
                    output.Add(teller);
                }
            }
            return output;
        }

        public List<ApplicationUser> GetAllWithEmailConfirmed()
        {
            return db.Users.Where(u => u.EmailConfirmed == true).ToList();
        }

    }
}
