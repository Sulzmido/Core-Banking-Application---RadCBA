using RadCBA.Core.Models;
using RadCBA.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Logic
{
    public class TellerMgtLogic
    {
        TellerMgtRepository tellerMgtRepo = new TellerMgtRepository();
        UserRepository userRepo = new UserRepository();
        GlAccountRepository glActRepo = new GlAccountRepository();

        public List<TillToUser> ExtractAllTellerInfo()
        {
            var output = new List<TillToUser>();
            var tellersWithTill = tellerMgtRepo.GetAll();
            var tellersWithoutTill = userRepo.TellersWithoutTill();

            //adding all tellers without a till account
            foreach (var teller in tellersWithoutTill)
            {
                output.Add(new TillToUser { UserId = teller.Id, GlAccountID = 0 });
            }
            //adding all tellers with a till account
            output.AddRange(tellersWithTill);
            return output;
        }

        public List<ApplicationUser> ExtractTellersWithoutTill()
        {
            return userRepo.TellersWithoutTill();
        }

        public List<GlAccount> ExtractTillsWithoutTeller()
        {
            return glActRepo.TillsWithoutTeller();
        }
    }
}
