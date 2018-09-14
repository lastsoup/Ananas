using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ananas.Web.Mvc.Extensions;
using System.Data.SqlClient;
using System.IO;

namespace Ananas.Web.Mvc.Models
{
    public class UserFactory
    {
        public List<UserInfo> GetAll()
        {

            List<UserInfo> dals = new List<UserInfo>();
            string sql = "select * from Ananas_User";
            SqlDataReader data =SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                UserInfo dal = new UserInfo()
                {
                    ID = data["ID"].ToString(),
                    Name = data["Name"].ToString(),
                    sName = data["sName"].ToString(),
                    Password = data["Password"].ToString(),
                    Role = (int)data["RoleID"],
                    Skin = data["Skin"].ToString(),
                    Email = data["Email"].ToString(),
                    Introduction = data["Introduction"].ToString(),
                    ImageUrl = data["ImageUrl"].ToString(),
                    Projects = data["Projects"].ToString(),
                    Background = data["Background"].ToString()
                };

                dals.Add(dal);
            }
            return dals;
        }

        public UserInfo FirstOne(string sql)
        {
            DataTable dt = SqlServerDBHelper.GetDataSet(sql);
            if (dt.Rows.Count == 0)
                return null;
            var data = dt.Select().First();
            UserInfo dal = new UserInfo(){
                ID = data["ID"].ToString(),
                Name = data["Name"].ToString(),
                sName = data["sName"].ToString(),
                Password = data["Password"].ToString(),
                Role = (int)data["RoleID"],
                Skin = data["Skin"].ToString(),
                Email = data["Email"].ToString(),
                Introduction = data["Introduction"].ToString(),
                ImageUrl = GetImageUrl(data["ImageUrl"].ToString()),
                Projects = data["Projects"].ToString(),
                Background = data["Background"].ToString()
            };
            return dal;
        }

        public string GetImageUrl(string imgurl)
        {
            if (String.IsNullOrWhiteSpace(imgurl))
            {
                imgurl = "/Content/Images/tongxiang.jpg";
            }
            //var filepath = System.Web.Hosting.HostingEnvironment.MapPath(imgurl);
            var filepath = System.AppDomain.CurrentDomain.BaseDirectory.ToString()+imgurl;
            var imageurl = File.Exists(filepath) ? imgurl : "/Content/Images/tongxiang.jpg";
            return imageurl;
        }

        public int UpdateInfo(string sql){
            return SqlServerDBHelper.ExecuteCommand(sql);
        }


    }
}
