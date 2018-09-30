using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Ananas.Web.Mvc.Extensions;
using System.Data.SqlClient;
using fastJSON;

namespace Ananas.Web.Mvc.Examples.Models
{
    public class FileFactory
    {
        public List<FilesInfo> GetAll()
        {

            List<FilesInfo> dals = new List<FilesInfo>();
            string sql = "select * from Ananas_File";
            SqlDataReader data =SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                FilesInfo dal = new FilesInfo()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Name = data["Name"].ToString(),
                    State = data["State"].ToString(),
                    Type = data["Type"].ToString(),
                    Extension = data["Extension"].ToString(),
                    CreateDate = (DateTime)data["CreateDate"],
                    Url = data["Url"].ToString(),
                    Describe = data["Describe"].ToString(),
                    Owner = data["Owner"].ToString(),
                    ParentID = data["ParentID"].ToString(),
                    IsPrivate = data["IsPrivate"].ToString(),
                    Custom = data["Custom"].ToString(),
                    Remark = data["Remark"].ToString(),
                    CoverUrl = data["CoverUrl"].ToString()
                };

                dals.Add(dal);
            }
            return dals;
        }

        public List<FilesInfo> GetFileWhere(string strwhere)
        {

            List<FilesInfo> dals = new List<FilesInfo>();
            string sql = "select * from Ananas_File where "+ strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                FilesInfo dal = new FilesInfo()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Name = data["Name"].ToString(),
                    State = data["State"].ToString(),
                    Type = data["Type"].ToString(),
                    Extension = data["Extension"].ToString(),
                    CreateDate=(DateTime)data["CreateDate"],
                    Url = data["Url"].ToString(),
                    Describe = data["Describe"].ToString(),
                    Owner = data["Owner"].ToString(),
                    ParentID = data["ParentID"].ToString(),
                    IsPrivate = data["IsPrivate"].ToString(),
                    Custom = data["Custom"].ToString(),
                    Remark = data["Remark"].ToString(),
                    CoverUrl = data["CoverUrl"].ToString()
                };

                dals.Add(dal);
            }
            return dals;
        }

        //特殊处理图片子表
        public List<FilesInfo> GetImageWithDetail(string strwhere)
        {

            List<FilesInfo> dals = new List<FilesInfo>();
            string sql = "select * from Ananas_File where " + strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                FilesInfo dal = new FilesInfo()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Name = data["Name"].ToString(),
                    State = data["State"].ToString(),
                    Type = data["Type"].ToString(),
                    Extension = data["Extension"].ToString(),
                    CreateDate=(DateTime)data["CreateDate"],
                    Url = data["Url"].ToString(),
                    Describe = data["Describe"].ToString(),
                    Owner = data["Owner"].ToString(),
                    ParentID = data["ParentID"].ToString(),
                    IsPrivate = data["IsPrivate"].ToString(),
                    Custom = data["Custom"].ToString(),
                    FirstSize = this.GetFirstSize(data["Custom"].ToString()),
                    SizeList = this.GetSizeList(data["Custom"].ToString()),
                    Remark = data["Remark"].ToString(),
                    CoverUrl = data["CoverUrl"].ToString()
                };

                dals.Add(dal);
            }
            return dals;
        }

        public List<FileSize> GetSizeList(string parm)
        {
            if (parm == "")
                return null;
            string[] sizearry = parm.Split(';');
            List<FileSize> sizelist = new List<FileSize>();
            foreach (string size in sizearry)
            {
                FileSize newsize = JSON.ToObject<FileSize>(size);
                sizelist.Add(newsize);
            }
            return sizelist;
        }

        public string GetFirstSize(string parm)
        {
            if (parm == "")
                return "";
            string[] sizearry = parm.Split(';');
            FileSize newsize = null;
            if (sizearry[0] != "")
            {
                newsize = JSON.ToObject<FileSize>(sizearry[0]);
            }
            return "_" + newsize.width + "x" + newsize.height;
        }

        //处理收藏目录的文件计算
        public List<FilesInfo> GetFileWithDetail(string strwhere)
        {

            List<FilesInfo> dals = new List<FilesInfo>();
            string sql = "select * from Ananas_File where " + strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                FilesInfo dal = new FilesInfo()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Name = data["Name"].ToString(),
                    State = data["State"].ToString(),
                    Type = data["Type"].ToString(),
                    Extension = data["Extension"].ToString(),
                    CreateDate = (DateTime)data["CreateDate"],
                    Url = data["Url"].ToString(),
                    Describe = data["Describe"].ToString(),
                    Owner = data["Owner"].ToString(),
                    ParentID = data["ParentID"].ToString(),
                    IsPrivate = data["IsPrivate"].ToString(),
                    Custom = data["Custom"].ToString(),
                    Remark = data["Remark"].ToString(),
                    CoverUrl = data["CoverUrl"].ToString(),
                    DetailCount = SqlServerDBHelper.GetScalar("select COUNT(*) from Ananas_File where ParentID='" + data["ID"].ToString() + "'"),
                    FirstFile = this.FirstOne("select * from Ananas_File where ParentID='" + data["ID"].ToString() + "'")
                };

                dals.Add(dal);
            }
            return dals;
        }

        public FilesInfo FirstOne(string sql)
        {
            DataTable dt = SqlServerDBHelper.GetDataSet(sql);
            if (dt.Rows.Count == 0)
                return null;
            var data = dt.Select(String.Empty, "CreateDate desc").First();
            FilesInfo dal = new FilesInfo()
            {
                ID = data["ID"].ToString(),
                UID = data["UID"].ToString(),
                Name = data["Name"].ToString(),
                State = data["State"].ToString(),
                Type = data["Type"].ToString(),
                Extension = data["Extension"].ToString(),
                CreateDate = (DateTime)data["CreateDate"],
                Url = data["Url"].ToString(),
                Describe = data["Describe"].ToString(),
                Owner = data["Owner"].ToString(),
                ParentID = data["ParentID"].ToString(),
                IsPrivate = data["IsPrivate"].ToString(),
                Custom = data["Custom"].ToString(),
                FirstSize = this.GetFirstSize(data["Custom"].ToString()),
                Remark = data["Remark"].ToString(),
                CoverUrl = data["CoverUrl"].ToString()
            };
            return dal;
        }

        public FilesInfo FirstImageOne(string sql)
        {
            DataTable dt = SqlServerDBHelper.GetDataSet(sql);
            if (dt.Rows.Count == 0)
                return null;
            var data = dt.Select().First();
            FilesInfo dal = new FilesInfo()
            {
                ID = data["ID"].ToString(),
                UID = data["UID"].ToString(),
                Name = data["Name"].ToString(),
                State = data["State"].ToString(),
                Type = data["Type"].ToString(),
                Extension = data["Extension"].ToString(),
                CreateDate = (DateTime)data["CreateDate"],
                Url = data["Url"].ToString(),
                Describe = data["Describe"].ToString(),
                Owner = data["Owner"].ToString(),
                ParentID = data["ParentID"].ToString(),
                IsPrivate = data["IsPrivate"].ToString(),
                Custom = data["Custom"].ToString(),
                Size=data["Custom"].ToString().Split(';'),
                Remark = data["Remark"].ToString(),
                CoverUrl = data["CoverUrl"].ToString()
            };
            return dal;
        }

        public int UpdateInfo(string sql){
            return SqlServerDBHelper.ExecuteCommand(sql);
        }


    }
}
