using System.Collections.Generic;
using System.Linq;
using System.Data;
using Ananas.Web.Mvc.Extensions;
using System.Data.SqlClient;
using Ananas.Web.Mvc.Base;

namespace Ananas.Web.Mvc.Examples.Models
{
    public class HomeFactory
    {

        public List<HomeList> SetDefaultHomeList(string uid, int homegroup)
        {
            List<HomeList> listshome = new List<HomeList>();
            for (int i = 0; i < homegroup; i++)
            {
                HomeList newlist = new HomeList();
                listshome.Add(newlist);
            }
            return SetDefaultHome(uid, listshome);
        }

        public HomeList SetGropHome(int count)
        {
            HomeList homelist = new HomeList();
            for (int i = 0; i < count; i++)
            {
                HomeInfo newhome = new HomeInfo();
                homelist.Blocks.Add(newhome);
            }

            return homelist;
        }


        public List<HomeList> SetDefaultHome(string uid, List<HomeList> listshome)
        {
            listshome[0] = SetGropHome(4);
            listshome[1] = SetGropHome(5);
            listshome[2] = SetGropHome(5);
            listshome[3] = SetGropHome(5);
            HomeInfo defaulthome00 = new HomeInfo()
            {
                ID="",
                Name = "studio",
                Value = "简介",
                PluginType = PluginType.TextLink.ToString(),
                Url = "/user/" + uid + "/userview",
                Color = "#00a300",
                HeightClass = "nrheight1"
            };
            HomeInfo defaulthome01 = new HomeInfo()
            {
                ID = "",
                Name = "blank",
                PluginType = PluginType.Blank.ToString(),
                Color = "#5839bf",
                HeightClass = "nrheight1"
            };
            HomeInfo defaulthome02 = new HomeInfo()
            {
                ID = "",
                Name = "imageview",
                PluginType = PluginType.ImageView.ToString(),
                Color = "#009aac",
                Plugin = "a4e4845a498442938c267d2a0cfe31e3",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome03 = new HomeInfo()
            {
                ID = "",
                Name = "copyright",
                Value = "Copyright &copy;2014 Ananas Studio",
                PluginType = PluginType.TextLink.ToString(),
                Url = "javascript:;",
                Color = "#a000a8",
                ContentStyle = "font-size:12px;font-weight:normal;font-family: Arial,Helvetica,sans-serif;",
                HeightClass = "nrheight1"
            };

            listshome[0].Blocks[0] = defaulthome00;
            listshome[0].Blocks[1] = defaulthome01;
            listshome[0].Blocks[2] = defaulthome02;
            listshome[0].Blocks[3] = defaulthome03;

            HomeInfo defaulthome10 = new HomeInfo()
            {
                ID = "",
                Name = "clock",
                PluginType = PluginType.ClockPlugin.ToString(),
                Color = "#2d87ef",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome11 = new HomeInfo()
            {
                ID = "",
                Name = "works",
                Value = "作品",
                PluginType = PluginType.TextLink.ToString(),
                Url = "/user/" + uid + "/userview/works",
                Color = "#d9532c",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome12 = new HomeInfo()
            {
                ID = "",
                Name = "projects",
                Value = "工作经历",
                PluginType = PluginType.TextLink.ToString(),
                Url = "/user/" + uid + "/userview/projects",
                Color = "#a000a8",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome13 = new HomeInfo()
            {
                ID = "",
                Name = "weibo",
                PluginType = PluginType.IconLink.ToString(),
                Url = "http://weibo.com/u/2381428644",
                Color = "#009d00",
                ContentStyle = "fa-weibo",
                WidthStyle = "float: left; width: 115px;",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome14 = new HomeInfo()
            {
                ID = "",
                Name = "tencent",
                PluginType = PluginType.IconLink.ToString(),
                Url = "http://t.qq.com/weiboTfff4053",
                Color = "#009d00",
                ContentStyle = "fa-tencent-weibo",
                WidthStyle = "float: right; width: 114px;",
                HeightClass = "nrheight1"
            };
            listshome[1].Blocks[0] = defaulthome10;
            listshome[1].Blocks[1] = defaulthome11;
            listshome[1].Blocks[2] = defaulthome12;
            listshome[1].Blocks[3] = defaulthome13;
            listshome[1].Blocks[4] = defaulthome14;

            HomeInfo defaulthome23 = new HomeInfo()
            {
                ID = "",
                Name = "qq",
                PluginType = PluginType.IconLink.ToString(),
                Url = "http://wpa.qq.com/msgrd?v=3&uin=2208861110&site=qq&menu=yes",
                Color = "#a000a8",
                ContentStyle = "fa-qq",
                WidthStyle = "float: left; width: 115px;",
                HeightClass = "nrheight1"
            };
            HomeInfo defaulthome24 = new HomeInfo()
            {
                WidthStyle = "float: right; width: 114px;",
                HeightClass = "nrheight1"
            };
            listshome[2].Blocks[3] = defaulthome23;
            listshome[2].Blocks[4] = defaulthome24;

            HomeInfo defaulthome30 = new HomeInfo()
            {
                ID = "",
                Name = "musicplay",
                PluginType = PluginType.MusicPlay.ToString(),
                Color = "#93d008",
                Plugin = "53bce25f525c47388e58809897b7cc7c",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome31 = new HomeInfo()
            {
                ID = "",
                Name="blank",
                PluginType = PluginType.Blank.ToString(),
                Color = "#094db5",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome32 = new HomeInfo()
            {
                ID = "",
                Name = "blank",
                PluginType = PluginType.Blank.ToString(),
                Color = "#009aac",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome33 = new HomeInfo()
            {
                ID = "",
                Name = "blank",
                PluginType = PluginType.Blank.ToString(),
                Color = "#ff5400",
                WidthStyle = "float: left; width: 115px;",
                HeightClass = "nrheight1"
            };

            HomeInfo defaulthome34 = new HomeInfo()
            {
                ID = "",
                Name = "weather",
                Value="南京",
                PluginType = PluginType.WeatherPlugin.ToString(),
                Color = "#5839bf",
                WidthStyle = "float: right; width: 114px;",
                HeightClass = "nrheight1"
            };
            listshome[3].Blocks[0] = defaulthome30;
            listshome[3].Blocks[1] = defaulthome31;
            listshome[3].Blocks[2] = defaulthome32;
            listshome[3].Blocks[3] = defaulthome33;
            listshome[3].Blocks[4] = defaulthome34;
            return listshome;
        }

        public List<HomeInfo> GetHomeWhere(string strwhere)
        {

            List<HomeInfo> dals = new List<HomeInfo>();
            string sql = "select * from Ananas_Home where " + strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                HomeInfo dal = new HomeInfo()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Name = data["Name"].ToString(),
                    Value = data["Value"].ToString(),
                    Type = data["Type"].ToString(),
                    Url = data["Url"].ToString(),
                    Color = data["Color"].ToString(),
                    PluginType = data["PluginType"].ToString(),
                    Plugin = data["Plugin"].ToString(),
                    WidthStyle = data["WidthStyle"].ToString(),
                    HeightClass = data["HeightClass"].ToString(),
                    GroupID = data["GroupID"].ToString(),
                    Order = data["Order"].ToString(),
                    ContentStyle = data["ContentStyle"].ToString(),
                    Remark = data["Remark"].ToString()
                };

                dals.Add(dal);
            }
            return dals;
        }

        public List<HomeList> GetHomeWhereWithBlock(string strwhere)
        {

            List<HomeList> dals = new List<HomeList>();
            string sql = "select * from Ananas_Home where " + strwhere;
            SqlDataReader data = SqlServerDBHelper.GetReader(sql);
            while (data.Read())
            {
                HomeList dal = new HomeList()
                {
                    ID = data["ID"].ToString(),
                    UID = data["UID"].ToString(),
                    Type = data["Type"].ToString(),
                    Order = data["Order"].ToString(),
                    GroupClass = data["Value"].ToString(),
                    Blocks = this.GetHomeWhere("GroupID ='" + data["ID"].ToString() + "'").OrderBy(d => d.Order).ToList()
                };

                dals.Add(dal);
            }
            return dals;
        }
        public HomeInfo FirstOne(string sql)
        {
            DataTable dt = SqlServerDBHelper.GetDataSet(sql);
            if (dt.Rows.Count == 0)
                return null;
            var data = dt.Select().First();
            HomeInfo dal = new HomeInfo(){
                ID = data["ID"].ToString(),
                UID = data["UID"].ToString(),
                Name = data["Name"].ToString(),
                Value = data["Value"].ToString(),
                Type = data["Type"].ToString(),
                Url = data["Url"].ToString(),
                Color = data["Color"].ToString(),
                PluginType = data["PluginType"].ToString(),
                Plugin = data["Plugin"].ToString(),
                WidthStyle = data["WidthStyle"].ToString(),
                HeightClass = data["HeightClass"].ToString(),
                GroupID = data["GroupID"].ToString(),
                Order = data["Order"].ToString(),
                ContentStyle = data["ContentStyle"].ToString(),
                Remark = data["Remark"].ToString()
            };
            return dal;
        }

        public int UpdateInfo(string sql){
            return SqlServerDBHelper.ExecuteCommand(sql);
        }


    }
}
