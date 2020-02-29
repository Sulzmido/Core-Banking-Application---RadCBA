using RadCBA.Core.Models;
using RadCBA.Core.ViewModels.FinancialReportViewModel;
using RadCBA.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Logic
{
    public class BalanceSheetLogic
    {
        BalanceSheetRepository bsRepo = new BalanceSheetRepository();

        public List<GlAccount> GetAssetAccounts()
        {
            return bsRepo.GetAssetAccounts();
        }

        public List<GlAccount> GetCapitalAccounts()
        {
            return bsRepo.GetCapitalAccounts();
        }

        public List<LiabilityViewModel> GetLiabilityAccounts()
        {
            return bsRepo.GetLiabilityAccounts();
        }
    }
}
