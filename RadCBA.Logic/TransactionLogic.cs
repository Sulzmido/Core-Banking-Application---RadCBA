using RadCBA.Core.Models;
using RadCBA.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Logic
{
    public class TransactionLogic
    {
        TransactionRepository transRepo = new TransactionRepository();

        public List<Transaction> GetTrialBalanceTransactions(DateTime startDate, DateTime endDate)
        {
            return transRepo.GetTrialBalanceTransactions(startDate, endDate);
        }
    }
}
