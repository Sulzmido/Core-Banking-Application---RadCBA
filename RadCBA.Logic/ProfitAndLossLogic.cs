using RadCBA.Core.Models;
using RadCBA.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Logic
{
    public class ProfitAndLossLogic
    {
        ProfitAndLossRepository plRepo = new ProfitAndLossRepository();

        public List<ExpenseIncomeEntry> GetEntries()
        {
            return plRepo.GetEntries();
        }

        public List<ExpenseIncomeEntry> GetEntries(DateTime startDate, DateTime endDate)
        {
            return plRepo.GetEntries(startDate, endDate);
        }
    }
}
