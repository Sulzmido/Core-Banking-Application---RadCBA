using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Core.Models
{
    public enum PostStatus
    {
        Pending, Declined, Approved
    }
    public class GLToAuth
    {
        public int ID { get; set; }

        public int? DrGlAccountID { get; set; }
        public virtual GlAccount DrGlAccount { get; set; }

        public int? CrGlAccountID { get; set; }
        public virtual GlAccount CrGlAccount { get; set; }

        public string InitiatorId { get; set; }

        public decimal Amount { get; set; }

        public PostStatus PostStatus { get; set; }

        public DateTime Date { get; set; }
        public string Narration { get; set; }
    }
}
