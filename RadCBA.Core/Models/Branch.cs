using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RadCBA.Core.Models
{
    public enum BranchStatus
    {
        Closed, Open
    }
    public class Branch
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
        public long SortCode { get; set; } 

        public BranchStatus Status { get; set; }
    }
}
