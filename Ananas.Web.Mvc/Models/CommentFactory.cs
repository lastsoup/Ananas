using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ananas.Web.Mvc.Extensions;
using System.Data.SqlClient;

namespace Ananas.Web.Mvc.Models
{
    public class CommentFactory
    {
        public List<CommentInfo> GetAll()
        {

            List<CommentInfo> dals = new List<CommentInfo>();
            string sql = "select * from Ananas_Comment";
            SqlDataReader data =SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                CommentInfo dal = new CommentInfo()
                {
                    ID = data["ID"].ToString(),
                    WorkID = data["WorkID"].ToString(),
                    Content = data["Content"].ToString(),
                    IP = data["IP"].ToString(),
                    Type = data["Type"].ToString(),
                    Remark = data["Remark"].ToString(),
                    UID = data["UID"].ToString(),
                    CreateDate = ((DateTime)data["CreateDate"]).ToString(@"yyyy-MM-dd HH:mm")
                };

                dals.Add(dal);
            }
            return dals;
        }

        public List<CommentInfo> GetCommentWhere(string strwhere)
        {

            List<CommentInfo> dals = new List<CommentInfo>();
            string sql = "select * from Ananas_Comment where " + strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                CommentInfo dal = new CommentInfo()
                {
                    ID = data["ID"].ToString(),
                    WorkID = data["WorkID"].ToString(),
                    Content = data["Content"].ToString(),
                    IP = data["IP"].ToString(),
                    Type = data["Type"].ToString(),
                    Remark = data["Remark"].ToString(),
                    UID = data["UID"].ToString(),
                    CreateDate = ((DateTime)data["CreateDate"]).ToString(@"yyyy-MM-dd HH:mm")
                };

                dals.Add(dal);
            }
            return dals;
        }

        public List<CommentInfo> GetCommentSql(string sql)
        {

            List<CommentInfo> dals = new List<CommentInfo>();
            //string sql = "select * from Ananas_Comment where " + strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                CommentInfo dal = new CommentInfo()
                {
                    ID = data["ID"].ToString(),
                    WorkID = data["WorkID"].ToString(),
                    Content = data["Content"].ToString(),
                    IP = data["IP"].ToString(),
                    Type = data["Type"].ToString(),
                    Remark = data["Remark"].ToString(),
                    UID = data["UID"].ToString(),
                    CreateDate = ((DateTime)data["CreateDate"]).ToString(@"yyyy-MM-dd HH:mm")
                };

                dals.Add(dal);
            }
            return dals;
        }



        public CommentInfo FirstOne(string sql)
        {
            DataTable dt = SqlServerDBHelper.GetDataSet(sql);
            if (dt.Rows.Count == 0)
                return null;
            var data = dt.Select().First();
            CommentInfo dal = new CommentInfo()
            {
                ID = data["ID"].ToString(),
                WorkID = data["WorkID"].ToString(),
                Content = data["Content"].ToString(),
                IP = data["IP"].ToString(),
                Type = data["Type"].ToString(),
                Remark = data["Remark"].ToString(),
                UID = data["UID"].ToString(),
                CreateDate = ((DateTime)data["CreateDate"]).ToString(@"yyyy-MM-dd HH:mm")
            };
            return dal;
        }

      

        public int UpdateInfo(string sql){
            return SqlServerDBHelper.ExecuteCommand(sql);
        }


    }
}
