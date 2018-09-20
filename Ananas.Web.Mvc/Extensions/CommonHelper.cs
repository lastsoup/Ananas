using System;
using System.Text;
using fastJSON;
using System.IO;
using System.IO.Compression;
using System.Net;
using Ananas.Web.Mvc.Models;
using System.Collections.Specialized;
using System.Reflection;
using System.ComponentModel;

namespace Ananas.Web.Mvc.Extensions
{
    /// <summary>
    /// 通用帮助类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 获取guid
        /// </summary>
        /// <returns></returns>
        public static string getGUID()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            string str = guid.ToString().Replace("-","");
            return str;
        }

        public static string GenerateIntID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            string str = BitConverter.ToUInt32(buffer, 8).ToString();
            return str;
        }

        public static string MD5Password(string pwd)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
           {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pwd));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
           }
        }
        
        /// <summary>
        /// 把Unicode解码为普通文字
        /// </summary>
        /// <param name="unicodeString">要解码的Unicode字符集</param>
        /// <returns>解码后的字符串</returns>
        public static string ConvertToGB(string unicodeString)
        {
            string[] strArray = unicodeString.Split(new string[] { @"/u" }, StringSplitOptions.None);
            string result = string.Empty;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].Trim() == "" || strArray[i].Length < 2 || strArray.Length <= 1)
                {
                    result += i == 0 ? strArray[i] : @"/u" + strArray[i];
                    continue;
                }
                for (int j = strArray[i].Length > 4 ? 4 : strArray[i].Length; j >= 2; j--)
                {
                    try
                    {
                        result += char.ConvertFromUtf32(Convert.ToInt32(strArray[i].Substring(0, j), 16)) + strArray[i].Substring(j);
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 把汉字字符转码为Unicode字符集
        /// </summary>
        /// <param name="strGB">要转码的字符</param>
        /// <returns>转码后的字符</returns>
        public static string ConvertToUnicode(string strGB)
        {
            char[] chs = strGB.ToCharArray();
            string result = string.Empty;
            foreach (char c in chs)
            {
                result += @"/u" + char.ConvertToUtf32(c.ToString(), 0).ToString("x");
            }
            return result;
        }

        public static string Serializer(string[] arrygroup)
        {
            var json = JSON.ToJSON(arrygroup);
            return Encode(Compress(json));
        }

        public static object Deserialize(string source)
        {
            
            var jsonString = Decompress(Decode(source));
            object json = JSON.ToObject(jsonString);
            return json;
        }

        public static string Encode(string target)
        {
            return target.Replace("/", "_").Replace("+", "-");
        }

        public static string Decode(string target)
        {
            return target.Replace("-", "+").Replace("_", "/");
        }

        public static string Compress(string instance)
        {
          
            byte[] binary = Encoding.UTF8.GetBytes(instance);
            byte[] compressed;

            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress))
                {
                    zip.Write(binary, 0, binary.Length);
                }

                compressed = ms.ToArray();
            }

            byte[] compressedWithLength = new byte[compressed.Length + 4];

            Buffer.BlockCopy(compressed, 0, compressedWithLength, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(binary.Length), 0, compressedWithLength, 0, 4);

            return Convert.ToBase64String(compressedWithLength);
        }

        private static byte[] DecodeByte(string value)
        {
            try
            {
                return Convert.FromBase64String(value);
            }
            catch (FormatException)
            {
                return new byte[0];
            }
        }

        public static string Decompress(string instance)
        {
            var compressed = DecodeByte(instance);

            if (compressed.Length < 4)
            {
                return string.Empty;
            }

            using (var stream = new MemoryStream())
            {
                var length = BitConverter.ToInt32(compressed, 0);
                stream.Write(compressed, 4, compressed.Length - 4);
                var binary = new byte[length];
                stream.Seek(0, SeekOrigin.Begin);

                using (var zip = new GZipStream(stream, CompressionMode.Decompress))
                {
                    try
                    {
                        zip.Read(binary, 0, binary.Length);

                        return Encoding.UTF8.GetString(binary);
                    }
                    catch (InvalidDataException)
                    {
                        return string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;
            // result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            // if (string.IsNullOrEmpty(result))
            // {
            //     result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            // }
            // if (string.IsNullOrEmpty(result))
            // {
            //     result = HttpContext.Current.Request.UserHostAddress;
            // }
            if (string.IsNullOrEmpty(result))
            {
                return "127.0.0.1";
            }
            return result;
        }



        public static long GetCurrentTimeStamp()
        {
            DateTime dt = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (long)(DateTime.Now - dt).TotalSeconds;
        }

        public static long GetTimeStamp(DateTime dt)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (long)(dt - dtStart).TotalSeconds;
        }

        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        #region 获取小说内容
        
        //获取网页内容
        public string theUrl;
        public string htmlCode;
        public int htmlErro;
        public void GetWebPage()
        {
            try
            {
                HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(theUrl);
                webRequest.Timeout = 30000;
                webRequest.ReadWriteTimeout = 30000;
                webRequest.Method = "GET";
                webRequest.UserAgent = "Mozilla/4.0";
                webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
                if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                    {
                        using (var zipStream =
                            new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                        {
                            using (StreamReader sr = new System.IO.StreamReader(zipStream, Encoding.Default))
                            {
                                htmlCode = sr.ReadToEnd();
                                sr.Close();
                            }

                        }
                        streamReceive.Close();
                    }
                }
                else
                {
                    using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive, Encoding.Default))
                        {
                            htmlCode = sr.ReadToEnd();
                            sr.Close();
                        }
                        streamReceive.Close();
                    }
                }
                webResponse.Close();
                htmlErro = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                htmlErro = -1;
            }
        }

        public string physicalPath;
        public void WriteWebPage()
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(theUrl);
                req.AllowAutoRedirect = true;
                req.KeepAlive = true;
                req.Proxy = null;
                //req.ReadWriteTimeout = 300000;
                Stream oStream = req.GetResponse().GetResponseStream();
                using (StreamReader respStreamReader = new StreamReader(oStream, Encoding.UTF8))
                {
                    string line = string.Empty;
                    while ((line = respStreamReader.ReadLine()) != null)
                    {

                        UTF8Encoding utf8 = new UTF8Encoding(false);
                        using (StreamWriter sw = new StreamWriter(physicalPath, true, utf8))
                        {
                            sw.WriteLine(line);
                        }

                    }
                }
                //req.Close();
                htmlErro = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                htmlErro = -1;
            }
        }

        public long pagenumber;
        public string leng;
        public void GetTxtPageContent()
        {
            try
            {
                System.Text.Encoding encoding = EncodingType.GetType(theUrl);
               
                //获取txt文件分页信息
                BigFileReader bfr = new BigFileReader(theUrl, encoding);
                if (encoding == Encoding.Default)
                {
                    encoding = Encoding.Unicode;
                }
                byte[] b = bfr.GetPage(pagenumber);
                htmlCode = encoding.GetString(b);
                htmlErro = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                htmlErro = -1;
            }
        }


        //存储下载小说的目录
        public static NameValueCollection CollectionMulu = new NameValueCollection();

        #endregion


        /// <summary>
        /// 转换文件大小
        /// </summary>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " Byte";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
            return m_strSize;
        }  

        //使用C#把发表的时间改为几个月,几天前,几小时前,几分钟前,或几秒前
        public static string DateStringFromNow(string strdt)
        {
            DateTime dt=Convert.ToDateTime(strdt);
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                if (span.TotalDays > 30)
                {
                    return
                    "1个月前";
                }
                else
                {
                    if (span.TotalDays > 14)
                    {
                        return
                        "2周前";
                    }
                    else
                    {
                        if (span.TotalDays > 7)
                        {
                            return
                            "1周前";
                        }
                        else
                        {
                            if (span.TotalDays > 1)
                            {
                                return
                                string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
                            }
                            else
                            {
                                if (span.TotalHours > 1)
                                {
                                    return
                                    string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                                }
                                else
                                {
                                    if (span.TotalMinutes > 1)
                                    {
                                        return
                                        string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                                    }
                                    else
                                    {
                                        if (span.TotalSeconds >= 1)
                                        {
                                            return
                                            string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                                        }
                                        else
                                        {
                                            return
                                            "1秒前";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //C#中使用TimeSpan计算两个时间的差值
        //可以反加两个日期之间任何一个时间单位。
        private string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }


        //说明：
       
        
        /// 
        /// 日期比较
        /// 
        /// 当前日期
        /// 输入日期
        /// 比较天数
        /// 大于天数返回true，小于返回false
        private bool CompareDate(string today, string writeDate, int n)
        {
            DateTime Today = Convert.ToDateTime(today);
            DateTime WriteDate = Convert.ToDateTime(writeDate);
            WriteDate = WriteDate.AddDays(n);
            if (Today >= WriteDate)
                return false;
            else
                return true;
        }

        public static UserInfo GetUserInfo(string uid)
        {
            UserInfo user = new UserFactory().FirstOne("select * from Ananas_User where ID='" + uid + "'");
            return user;
        }

      

    }

    public static class CommonHelperStatic
    {
        public static string GetEnumDes(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
    }
}
