using BSESAPI.BLL.Interfaces;
using BSESAPI.DataLayer.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BSESAPI.Controllers
{
    public class KioskController : ApiController
    {
        ICommon _common;
        public KioskController(ICommon common)
        {
            _common = common;
        }

        [HttpPost]
        public async Task<JObject> TransactionUpdate(TransactionsDB transactionsDbModel)
        {
            var result = await _common.TransactionUpdate(transactionsDbModel);
            return JObject.Parse(result);

        }
        [HttpPost]
        public async Task<JObject> BatchCloseUpdate(List<batch_closing_master> batchCloseModel)
        {
            var result = await _common.BatchCloseUpdate(batchCloseModel);
            return JObject.Parse(result);

        }
        [HttpPost]
        public async Task<JObject> MakeKioskAttendance(List<kiosk_attendance> kioskAttendanceModel)
        {
            var result = await _common.MakeKioskAttendance(kioskAttendanceModel);
            return JObject.Parse(result);

        }
    }
}
