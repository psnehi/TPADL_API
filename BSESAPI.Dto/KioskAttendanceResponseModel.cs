using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSESAPI.Dto
{
    public class KioskAttendanceResponseModel
    {
        public string KioskId { get; set; }
        public DateTime? LoginDateTime { get; set; }
        public DateTime? LogOutTime { get; set; }
        public string AtpmMchnIp { get; set; }
        public string Status { get; set; }
        public string TokenId { get; set; }
    }

}
