namespace Ananas.Web.Mvc.Examples.Models
{
    public class WorkInfo
    {
        public string ID { set; get; }
        public string UID { set; get; }
        public string Content { set; get; }
        public string Title { set; get; }
        public string Cover { set; get; }
        public string CreateDate { set; get; }
        public string Type { set; get; }
        //数据库以外新增类型
        public int CommentsCount { set; get; }
        public int ZanCount { set; get; }

        public string Reading { set; get; }
        public string Zaning { set; get; }
    }
}
