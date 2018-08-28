using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSES.Models
{
  public  class SMSModel
    {
        public List<string> MobileNumber { get; set; }
        public string MessageBody { get; set; }
    }
}
