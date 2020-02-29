using RadCBA.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Logic
{
    public interface IBranchLogic
    {
        long GetSortCode();
        bool IsBranchNameExists(string name);
    }
}
