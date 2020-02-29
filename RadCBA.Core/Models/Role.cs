using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RadCBA.Core.Models
{
    //public enum Claims
    //{
    //    BranchMgt, UserMgt, TellerMgt, CustomerMgt, CustomerAcccountMgt, GLPosting, TellerPosting, AccountConfigMgt, PostingAuth, RunEOD, RoleMgt, GLMgt, FinancialReport
    //}
    
    public class Role
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Role name should only contain characters with no white space"), MaxLength(40)]
        public string Name { get; set; }  

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
