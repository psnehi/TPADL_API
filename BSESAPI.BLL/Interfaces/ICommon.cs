using BSES.Models;
using BSESAPI.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSESAPI.BLL.Interfaces
{
  public interface ICommon
    {
     Task<string> SendEmail(SendEmailModel sendEmailModel);
     Task<string> MakeKioskAttendance(List<kiosk_attendance> kioskAttendanceModel);
     Task<string> TransactionUpdate(TransactionsDB transactionsDbModel);
     Task<string> BatchCloseUpdate(List<batch_closing_master> batchCloseModel);
     Task<string> SendSMS(SMSModel smsModel);
    }
}
