using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ananas.Web.Mvc.Models
{
    public class ViewModel
    {
        public string LoginName { set; get; }
        public UserInfo CurrentUser { set; get; }
        private UserInfo defaultviewuser = new UserInfo();
        public UserInfo ViewUser
        {
            get { return defaultviewuser; }
            set { defaultviewuser = value; }
        }
        public Dictionary<string, string> CurrentLang { set; get; }
        public WorkInfo CurrentWork { set; get; }
        public List<WorkInfo> works { set; get; }
        public FilesInfo CurrentNovel { set; get; }
        public List<FilesInfo> novels { set; get; }
        public List<List<FilesInfo>> groupnovels { set; get; }
        public List<FilesInfo> images { set; get; }
        public List<FilesInfo> codes { set; get; }
        public List<FilesInfo> albums { set; get; }
        public List<CommentInfo> PageComments { set; get; }

        private List<HomeList> defaulthome = new List<HomeList>();
        public List<HomeList> MyHome
        {
            get { return defaulthome; }
            set { defaulthome = value; }
        }
        //左侧菜单栏的位置索引号
        private int defaultpageindex = -1;
        public int PageIndex
        {
            get { return defaultpageindex; }
            set { defaultpageindex = value; }
        }

        private int defaultsubindex = -1;
        public int SubIndex
        {
            get { return defaultsubindex; }
            set { defaultsubindex = value; }
        }
        //不为空是work，为0添加
        private string defaultParentID = null;
        public string ParentID
        {
            get { return defaultParentID; }
            set { defaultParentID = value; }
        }
        public string Remark { set; get; }
        public object Storage { set; get; }
        public int PageCount { set; get; }
        public int PageSize { set; get; }
        public bool IsBool { set; get; }
        public string ZanID { set; get; }
        public int ZanCount { set; get; }
       
    }
}
