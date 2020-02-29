using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Core.ViewModels.RoleViewModels
{
    public class RoleClaimsViewModel
    {
        public int RoleId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Role Claims")]
        public ICollection<RoleClaim> RoleClaims { get; set; } 
    }
}
