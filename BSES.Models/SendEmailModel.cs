using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSES.Models
{
    public class SendEmailModel
    {
        public string ToAddress { get; set; }
        public string MessageBody { get; set; }
        public string EmailSubject { get; set; }
        public List<string> AttachFileName { get; set; }
        public List<byte[]> AttachmentFile { get; set; }
    }

}
