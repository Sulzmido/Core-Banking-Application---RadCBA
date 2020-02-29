using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCBA.Core.Models
{
    public class TellerToAuth
    {
        public int ID { get; set; }

        public int? DrGlAccountID { get; set; }
        public virtual GlAccount DrGlAccount { get; set; }

        public int? CrGlAccountID { get; set; }
        public virtual GlAccount CrGlAccount { get; set; }

        public int? DrCustomerAccountID { get; set; }
        public virtual CustomerAccount DrCustomerAccount { get; set; }

        public int? CrCustomerAccountID { get; set; }
        public virtual CustomerAccount CrCustomerAccount { get; set; }

        public string InitiatorId { get; set; }

        public decimal Amount { get; set; }

        public PostStatus PostStatus { get; set; }

        public DateTime Date { get; set; }
        public string Narration { get; set; }
    }
}
