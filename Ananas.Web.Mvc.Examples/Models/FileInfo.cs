using System;
using System.Collections.Generic;

namespace Ananas.Web.Mvc.Examples.Models
{
    public class FilesInfo
    {
        public string ID { set; get; }
        public string UID { set; get; }
        public string Name { set; get; }
        public string State { set; get; }
        public string Type { set; get; }
        public string Url { set; get; }
        public string Extension { set; get; }
        public string Owner { set; get; }
        public string Describe { set; get; }
        public DateTime CreateDate { set; get; }
        public string ParentID { set; get; }
        public string IsPrivate { set; get; }//0为不可见1为可见
        public string Custom { set; get; }//存储图片尺寸
        public string Remark { set; get; }
        public string CoverUrl { set; get; }
        
        //自定义的小说对象（sql数据库不存在）
        public NovelInfo NovelObject { set; get; }
        public int DetailCount { set; get; }
        public FilesInfo FirstFile { set; get; }
        public string[] Size { set; get; }
        public string FirstSize { set; get; }
        public List<FileSize> SizeList { set; get; }
    }

    public class NovelInfo
    {
        public string muluflag { set; get; }
        public string weburl { set; get; }
        public string webdomain { set; get; }
        public string webcontent { set; get; }
        public string testurl { set; get; }     
    }

    public class FileSize
    {
        public int width { set; get; }
        public int height { set; get; }
    }

    public class ResultData
    {
        public ErrInfo ErrInfo { get; set; }
        public Data Data { get; set; }
    }

    public class ErrInfo
    {
        public string no { get; set; }
    }

    public class Data
    {
        
        public string CodeString { get; set; }
        public string RememberedUserName { get; set; }
        public string Token { get; set; }
    }

}
