﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSES.Models
{
  public  class ResponseMessage
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
    }
}
