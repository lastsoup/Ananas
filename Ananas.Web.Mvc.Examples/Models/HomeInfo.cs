using System.Collections.Generic;

namespace Ananas.Web.Mvc.Examples.Models
{
    public class HomeList
    {
        public string ID { set; get; }
        public string UID { set; get; }
        public string Type { set; get; }
        public string Order { set; get; }
        private List<HomeInfo> defaulblock = new List<HomeInfo>();
        public List<HomeInfo> Blocks
        {
            get { return defaulblock; }
            set { defaulblock = value; }
        }
        private string defaultclass = "25";
        public string GroupClass
        {
            get { return defaultclass; }
            set { defaultclass = value; }
        }
    }
    public class HomeInfo
    {
        public string ID { set; get; }
        public string UID { set; get; }
        public string Name { set; get; }
        public string Value { set; get; }
        public string Type { set; get; }
        public string Url { set; get; }
        public string Color {set;get;}
        public string PluginType { set; get; }
        public string Plugin { set; get; }
        public string WidthStyle { set; get; }
        public string HeightClass { set; get; }
        public string Remark { set; get; }
        public string GroupID { set; get; }
        public string Order { set; get; }
        public string ContentStyle { set; get; }
    }
}
