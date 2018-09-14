using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ananas.Web.Mvc.Models
{
    public class CommentInfo
    {
        public string ID { set; get; }
        public string WorkID { set; get; }
        public string Content { set; get; }
        public string IP { set; get; }
        public string CreateDate { set; get; }
        public string Type { set; get; }
        public string Remark { set; get; }
        public string UID { set; get; }
    }
}
