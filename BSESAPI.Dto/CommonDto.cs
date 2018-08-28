using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSESAPI.Dto
{
    
    public class TransactionDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TransactionBYPLResponseModel UpdatedTransactionList { get; set; }
    }

    public class BatchCloseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BatchCloseResponseModel> UpdatedBatchCloseResponse { get; set; }
    }

    public class KioskAttendanceDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<KioskAttendanceResponseModel> UpdatedKioskAttendanceResponse { get; set; }
    }
}
