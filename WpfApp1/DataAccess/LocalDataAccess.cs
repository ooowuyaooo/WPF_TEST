using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WpfApp1.Common;
using WpfApp1.DataAccess.DataEntity;

namespace WpfApp1.DataAccess
{
    public class LocalDataAccess
    {
        private static LocalDataAccess instance;
        private LocalDataAccess()
        {

        }
        public static LocalDataAccess GetInstance()
        {
            return instance ?? (instance = new LocalDataAccess());
        }

        MySqlConnection conn;
        MySqlCommand comm;
        MySqlDataAdapter adapter;

        private void Dispose()
        {
            if(adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        private bool DBConnection()
        {
            if (conn == null)
                conn = new MySqlConnection("server=127.0.0.1;port=3306;user=root;password=123456; database=test;Allow User Variables=True");
            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public UserEntity CheckUserInfo(string userName,string pwd)
        {

            try {
                if (DBConnection())
                {
                    string userSql = "select * from users where user_name=@username and password=@pwd and is_validation=1";
                    adapter = new MySqlDataAdapter(userSql, conn);
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@user_name", MySqlDbType.VarChar)
                    {
                        Value =
                        userName
                    });
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@pwd", MySqlDbType.VarChar)
                    {
                        Value =
                        MD5Provider.GetMD5String(pwd + "@" + userName)
                    }); ;

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    //if (count <= 0)
                    //    throw new Exception("用户名密码不正确");

                    //DataRow dr = table.Rows[0];
                    //if (dr.Field<Int32>("is_can_login") == 0)
                    //    throw new Exception("当前用户无权限");


                    UserEntity userInfo = new UserEntity();
                    //userInfo.UserName = dr.Field<string>("user_name");
                    //userInfo.RealName = dr.Field<string>("real_name");
                    //userInfo.Password = dr.Field<string>("password");
                    userInfo.UserName = "admin";
                    userInfo.RealName = "EdisZhang";
                    userInfo.Password = "123456";
                    return userInfo;
                        
                    } 
            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }
    }
}
