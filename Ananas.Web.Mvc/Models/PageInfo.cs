namespace Ananas.Web.Mvc.Models
{
    public class PageInfo
    {
          //一页多少个
        public int length { get; set; }
        //第几个开始
        public int start { get; set; }
        //查询条件
        public string search { get; set; }
        //数据
        public object data { get; set; }
        //总数
        public int recordsTotal { get; set; }
        //数量
        public int recordsFiltered { get; set; }
    }
}