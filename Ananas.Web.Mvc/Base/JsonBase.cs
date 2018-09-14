using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ananas.Web.Mvc.Base
{
    public class JsonBase
    {
        public int State { set; get; }
        public bool IsSuccess { set; get; }
        public string Message { set; get; }
        public object BaseData { set; get; }
    }

  
}
