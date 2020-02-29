using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Core.ViewModels.RoleViewModels
{
    public class EditRoleViewModel
    {
        public int RoleId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Role name should only contain characters with no white space"), MaxLength(40)]
        public string Name { get; set; }

        public bool BranchMgt { get; set; }
        public bool UserMgt { get; set; }
        public bool TellerMgt { get; set; }
        public bool CustomerMgt { get; set; }
        public bool CustomerAccountMgt { get; set; }
        public bool GLPosting { get; set; }
        public bool TellerPosting { get; set; }
        public bool AccountConfigMgt { get; set; }
        public bool PostingAuth { get; set; }
        public bool RunEOD { get; set; }
        public bool RoleMgt { get; set; }
        public bool GLMgt { get; set; }
        public bool FinancialReport { get; set; }
    }
}
