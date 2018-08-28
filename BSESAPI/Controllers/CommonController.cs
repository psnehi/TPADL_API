using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BSES.Models;
using BSESAPI.BLL;
using BSESAPI.BLL.Interfaces;
using Newtonsoft.Json.Linq;

namespace BSESAPI.Controllers
{
    public class CommonController : ApiController
    {
        ICommon _common;
        public CommonController(ICommon common)
        {
            _common = common;
        }

        [HttpPost]
        public async Task<JObject> SendEmail(SendEmailModel model)
        {
            var result = await _common.SendEmail(model);
            return JObject.Parse(result);

        }

        [HttpPost]
        public async Task<JObject> SendSMS(SMSModel model)
        {
            var result = await _common.SendSMS(model);
            return JObject.Parse(result);

        }
    }
}
