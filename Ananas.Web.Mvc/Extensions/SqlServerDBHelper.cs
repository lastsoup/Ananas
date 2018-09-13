using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Ananas.Web.Mvc.Extensions
{
    public class SqlServerDBHelper
    {
        private static SqlConnection connection;
        public static SqlConnection Connection
        {
            get
            {
                string connectionstring =ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                if (connection == null)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }

        }
    public static int ExecuteCommand(string sql)
        {
            SqlCommand com = new SqlCommand(sql, Connection);
            int result = com.ExecuteNonQuery();
            return result;
        }
        public static int ExecuteCommand(string sql, params SqlParameter[] values)
        {
            SqlCommand com = new SqlCommand(sql, Connection);
            com.Parameters.AddRange(values);
            return com.ExecuteNonQuery();
        }
        public static int GetScalar(string sql)
        {
            SqlCommand com = new SqlCommand(sql, Connection);
            int result = int.Parse(com.ExecuteScalar().ToString());
            return result;
        }

       
        public static int GetScalar(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }
        public static DataTable GetDataSet(string sql)
        {
            DataSet dataset = new DataSet();
            SqlCommand com = new SqlCommand(sql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dataset);
            return dataset.Tables[0];
        }
        public static DataTable GetDataSet(string sql, params SqlParameter[] values)
        {
            DataSet dataset = new DataSet();
            SqlCommand com = new SqlCommand(sql, Connection);
            com.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dataset);
            return dataset.Tables[0];
        }
        public static SqlDataReader GetReader(string sql)
        {
            SqlCommand com = new SqlCommand(sql, Connection);
            SqlDataReader reader = com.ExecuteReader();
            return reader;
        }
        public static SqlDataReader GetReader(string sql, params SqlParameter[] values)
        {
            SqlCommand com = new SqlCommand(sql, Connection);
            com.Parameters.AddRange(values);
            SqlDataReader reader = com.ExecuteReader();
            return reader;
        }

        /// 存储过程
        public static int ExecuteProcCommand(string proc, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(proc, Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            int i = 0;
            SqlParameter pamReturn = cmd.Parameters.Add("@returnVal", SqlDbType.Int);
            pamReturn.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            if (pamReturn.Value.ToString() != null)
            {
                i = int.Parse(pamReturn.Value.ToString());
            }
            return i;
        }
        public static int GetProcScalar(string proc, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(proc, Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);

            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }
        public static SqlDataReader GetProcReader(string proc, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(proc, Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddRange(values);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public static DataTable GetProcDataSet(string safeSql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        
        public static DataTable GetProcDataSet(string proc, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand(proc, Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            return ds.Tables[0];
        }
//外键级联删除和更新
//alter table [Ananas].[dbo].[Ananas_Comment] add constraint FK_UpdateWorkID foreign key (WorkID) references [Ananas].[dbo].[Ananas_Work] (ID) ON UPDATE CASCADE
//alter table [Ananas].[dbo].[Ananas_Comment] add constraint FK_DeleteWorkID foreign key (WorkID) references [Ananas].[dbo].[Ananas_Work] (ID) ON DELETE CASCADE
//触发器删除和更新
//create trigger [dbo].[tgr_user_delete] on [Ananas].[dbo].[Ananas_User]
//for delete --删除触发
//as
//declare @oldID nvarchar(50), @newID nvarchar(50);
//    --更新前的数据
//    select @oldID = ID from deleted;
//    if (exists (select * from [Ananas].[dbo].[Ananas_Comment] where UID=@oldID))
//        begin
//            --更新后的数据
//            select @newID = ID from inserted;
//            delete from [Ananas].[dbo].[Ananas_Comment] where UID=@oldID;
//            print '级联删除数据成功！';
//        end
//    else
//        print '无需删除Comment表！';
//go
//---修改触发
//create trigger [dbo].[tgr_user_update] on [Ananas].[dbo].[Ananas_User]
//for update
//as
//declare @oldID nvarchar(50), @newID nvarchar(50);
//    --更新前的数据
//    select @oldID = ID from deleted;
//    if (exists (select * from [Ananas].[dbo].[Ananas_Comment] where UID=@oldID))
//        begin
//            --更新后的数据
//            select @newID = ID from inserted;
//            update [Ananas].[dbo].[Ananas_Comment] set UID = replace(UID, @oldID, @newID) where UID =@oldID;
//            print '级联修改数据成功！';
//        end
//    else
//        print '无需修改Comment表！';
//go

    }
}