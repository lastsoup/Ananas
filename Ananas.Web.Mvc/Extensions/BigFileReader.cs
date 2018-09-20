using System;
using System.Text;
using System.IO;

namespace Ananas.Web.Mvc.Extensions
{
    public class BigFileReader
    {
        private string m_fileName = null;
        private Int64 m_pageSize = -1;

        #region 设置文件名/页大小后自动生成

        private FileInfo m_fileInfo = null;
        private Int64 m_fileSize = -1;
        private Int64 m_pageCount = -1;
        private FileStream m_hsFileStream = null;

        #endregion

        #region 页读取相关

        private Int64 m_readerPageOffset = -1;
        private byte[] m_readerLastRead = null;

        #endregion
        public BigFileReader(string FileName)
        {
            this.FileName = FileName;
        }

        public BigFileReader(string FileName, System.Text.Encoding encoding)
        {
            if (encoding == Encoding.Default)
                Transform(FileName);//cqy转换文件编码为Unicode
            this.FileName = FileName;
        }

        /// <summary>
        /// 设置或获取文件名
        /// </summary>
        public string FileName
        {
            get { return m_fileName; }
            set
            {
                if (File.Exists(value))
                {
                    m_fileName = value;
                    m_hsFileStream = new FileStream(m_fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    m_fileInfo = new FileInfo(m_fileName);
                    m_fileSize = m_fileInfo.Length;
                    this.PageSize = 1024 * 3;
                    m_hsFileStream.Flush();
                    m_hsFileStream.Close();
                }
                else
                {
                    m_fileName = null;
                    m_fileInfo = null;
                    m_hsFileStream = null;
                    m_fileSize = -1;
                    m_pageCount = -1;
                }
            }
        }

        /// <summary>
        /// 设置或获取分页大小
        /// </summary>
        public Int64 PageSize
        {
            get { return m_pageSize; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("分页大小不能小于0字节");
                }
                m_pageSize = value;
                m_pageCount = m_fileSize / m_pageSize + ((m_fileSize % m_pageSize) > 0 ? 1 : 0);
            }
        }

        /// <summary>
        /// 获取文件长度(字节)
        /// </summary>
        public Int64 FileSize
        {
            get { return m_fileSize; }
        }

        /// <summary>
        /// 获取分页数
        /// </summary>
        public Int64 PageCount
        {
            get { return m_pageCount; }
        }

        /// <summary>
        /// 获取最后一次执行Read()时的结果
        /// </summary>
        public byte[] LastRead
        {
            get { return m_readerLastRead; }
        }

        public void Transform(string url)
        {

            try
            {
                FileInfo fi = new FileInfo(url);
                string oldFilename = fi.FullName;
                FileStream fs = new FileStream(oldFilename, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fs, System.Text.Encoding.Default);
                string newFileName = oldFilename.Substring(0, oldFilename.LastIndexOf(".")) + "_"+fi.Extension;
                File.WriteAllText(newFileName, streamReader.ReadToEnd(), Encoding.Unicode);
                streamReader.Close();
                fs.Close();
                File.Delete(url);
                FileInfo finew = new FileInfo(newFileName);
                finew.MoveTo(oldFilename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获得文件在设定页处的内容
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public byte[] GetPage(Int64 pageNumber)
        {
            //if (pageNumber < 0 || pageNumber >= this.PageCount)
            //{
            //    throw new ArgumentOutOfRangeException("pageNumber", "设定页超出了文件范围");
            //}
            if (pageNumber == -1)
            {
                this.PageSize = m_fileSize;
                pageNumber = 0;
            }
            
            using (FileStream fileStream = new FileStream(m_fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (fileStream == null || !fileStream.CanSeek || !fileStream.CanRead)
                {
                    return null;
                }
                Int64 offsetStart = (Int64)pageNumber * (Int64)this.PageSize;
                Int64 offsetEnd = offsetStart + this.PageSize - 1;

                if (pageNumber >= this.PageCount - 1)
                {
                    offsetEnd = this.FileSize - 1;
                }

                byte[] temp = new byte[offsetEnd - offsetStart + 1];

                fileStream.Seek(offsetStart, SeekOrigin.Begin);
                int rd = fileStream.Read(temp, 0, (Int32)(offsetEnd - offsetStart + 1));
                fileStream.Flush();
                fileStream.Close();
                return temp;
            }

        }

        /// <summary>
        /// 快速游标读取一页
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            if (++m_readerPageOffset >= this.PageCount)
            {
                return false;
            }
            if (m_hsFileStream == null || !m_hsFileStream.CanSeek || !m_hsFileStream.CanRead)
            {
                return false;
            }

            Int64 offsetStart = (Int64)m_readerPageOffset * (Int64)this.PageSize;
            Int64 offsetEnd = offsetStart + this.PageSize - 1;
            if (m_readerPageOffset >= this.PageCount - 1)
            {
                offsetEnd = this.FileSize - 1;
            }

            m_readerLastRead = new byte[offsetEnd - offsetStart + 1];
            m_hsFileStream.Seek(offsetStart, SeekOrigin.Begin);
            m_hsFileStream.Read(LastRead, 0, (Int32)(offsetEnd - offsetStart + 1));
            return true;
        }

        /// <summary>
        /// 重设读取游标位置
        /// </summary>
        /// <returns></returns>
        public Int64 ResetReaderOffset()
        {
            return (m_readerPageOffset = -1);
        }


    }
}
