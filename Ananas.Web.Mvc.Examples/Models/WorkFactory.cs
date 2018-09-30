using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Ananas.Web.Mvc.Extensions;
using System.Data.SqlClient;
using Ananas.Web.Mvc.Base;

namespace Ananas.Web.Mvc.Examples.Models
{
    public class WorkFactory
    {
        public List<WorkInfo> GetAll()
        {

            List<WorkInfo> dals = new List<WorkInfo>();
            string sql = "select * from Ananas_Work";
            SqlDataReader data =SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                WorkInfo dal = new WorkInfo()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Content = data["Content"].ToString(),
                    Title = data["Title"].ToString(),
                    Type = data["Type"].ToString(),
                    Cover = data["Cover"].ToString(),
                    Reading = data["Reading"].ToString(),
                    Zaning = data["Zaning"].ToString(),
                    CreateDate = ((DateTime)data["CreateDate"]).ToString(@"yyyy-MM-dd HH:mm")
                };

                dals.Add(dal);
            }
            return dals;
        }

        public List<WorkInfo> GetWorksWhere(string strwhere)
        {

            List<WorkInfo> dals = new List<WorkInfo>();
            string sql = "select * from Ananas_Work where " + strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                WorkInfo dal = new WorkInfo()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Content = data["Content"].ToString(),
                    Title = data["Title"].ToString(),
                    Type = data["Type"].ToString(),
                    Cover = data["Cover"].ToString(),
                    Reading = data["Reading"].ToString(),
                    Zaning = data["Zaning"].ToString(),
                    CreateDate = ((DateTime)data["CreateDate"]).ToString(@"yyyy-MM-dd HH:mm")
                };

                dals.Add(dal);
            }
            return dals;
        }

        //特殊处理获取子表
        public List<WorkInfo> GetWorksWhereWithDetail(string strwhere)
        {

            List<WorkInfo> dals = new List<WorkInfo>();
            string sql = "select * from Ananas_Work where " + strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                WorkInfo dal = new WorkInfo()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Content = data["Content"].ToString(),
                    Title = data["Title"].ToString(),
                    Type = data["Type"].ToString(),
                    Cover = data["Cover"].ToString(),
                    Reading = data["Reading"].ToString(),
                    Zaning = data["Zaning"].ToString(),
                    ZanCount = SqlServerDBHelper.GetScalar("select COUNT(*) from Ananas_Comment where WorkID='" + data["ID"].ToString() + "' and Type='" + CommentType.Zan + "'"),
                    CommentsCount = SqlServerDBHelper.GetScalar("select COUNT(*) from Ananas_Comment where WorkID='" + data["ID"].ToString() + "' and Type='"+ CommentType.Commrnt+"'"),
                    CreateDate = ((DateTime)data["CreateDate"]).ToString(@"yyyy-MM-dd HH:mm")
                };

                dals.Add(dal);
            }
            return dals;
        }

        public WorkInfo FirstOne(string sql)
        {
            DataTable dt = SqlServerDBHelper.GetDataSet(sql);
            if (dt.Rows.Count == 0)
                return null;
            var data = dt.Select().First();
            WorkInfo dal = new WorkInfo()
            {
                ID = data["ID"].ToString(),
                UID = data["UID"].ToString(),
                Content = data["Content"].ToString(),
                Title = data["Title"].ToString(),
                Type = data["Type"].ToString(),
                Cover = data["Cover"].ToString(),
                Reading = data["Reading"].ToString(),
                Zaning = data["Zaning"].ToString(),
                CreateDate = ((DateTime)data["CreateDate"]).ToString(@"yyyy-MM-dd HH:mm")
            };
            return dal;
        }

        public int UpdateInfo(string sql){
            return SqlServerDBHelper.ExecuteCommand(sql);
        }


    }
}
