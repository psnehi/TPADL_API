using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSESAPI.Dto
{
    public class BatchCloseResponseModel
    {
        public string KioskId { get; set; }
        public System.DateTime BatchCloseDateTime { get; set; }
        public int ReportBatchNo { get; set; }
        public string TokenId { get; set; }
        public string Status { get; set; }
    }
}
