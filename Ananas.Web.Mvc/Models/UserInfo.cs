using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ananas.Web.Mvc.Models
{
    public class UserInfo
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public string sName { set; get; }
        public string Password { set; get; }
        public int Role { set; get; }
        public string Skin {set;get;}
        public string Email { set; get; }
        public string Introduction { set; get; }
        public string Projects { set; get; }
        public string ImageUrl { set; get; }
        public string Background { set; get; }
        
    }
}
