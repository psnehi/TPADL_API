using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSESAPI.Dto
{
    public class TransactionBYPLResponseModel
    {
        public string TerminalID { get; set; }
        public Nullable<decimal> RCPTNO { get; set; }
        public string CANo { get; set; }
        public Nullable<System.DateTime> PAYMENTDATE { get; set; }
        public string TRANSACTION_ID { get; set; }
        public string UpdateToken { get; set; }
        public string UpdateStatus { get; set; }
    }
}
