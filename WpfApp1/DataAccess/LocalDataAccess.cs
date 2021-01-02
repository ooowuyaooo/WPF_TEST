using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using WpfApp1.Common;
using WpfApp1.DataAccess.DataEntity;
using WpfApp1.Model;

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
                conn = new MySqlConnection("server=rm-uf6a477d150fh94852o.mysql.rds.aliyuncs.com;port=3306;user=zhangyi;password=Goodluck2020!; database=testdb;Allow User Variables=True");
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

        public List<CourseSeriesModel> GetCoursePlayRecord()
        {
            try
            {
                List<CourseSeriesModel> cModelList = new List<CourseSeriesModel>();
                if (DBConnection())
                {
                    string userSql = @"select a.course_name, a.course_id ,b.play_count,b.is_growing ,b.growing_rate ,c.platform_name from course a
left join play_record b
on a.course_id = b.course_id
left join platforms c
on b.platform_id = c.platform_id
order by a.course_id,c.platform_id";
                    adapter = new MySqlDataAdapter(userSql, conn);
                    

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    string courseID = "";
                    CourseSeriesModel cModel = null;
                    
                    foreach(DataRow dr in table.AsEnumerable())
                    {
                        string tempID = dr.Field<string>("course_id");
                        if (courseID != tempID)
                        {
                            courseID = tempID;
                            cModel = new CourseSeriesModel();
                            cModelList.Add(cModel);

                            cModel.CourseName = dr.Field<string>("course_name");
                            cModel.SeriesCollection = new LiveCharts.SeriesCollection();
                            cModel.SeriesList = new System.Collections.ObjectModel.ObservableCollection<SeriesModel>();

                        }

                        if(cModel!=null)
                        {
                            cModel.SeriesCollection.Add(new PieSeries
                            {
                                Title = dr.Field<string>("platform_name"),
                                Values = new ChartValues<ObservableValue> { new ObservableValue((double)dr.Field<decimal>("play_count")) },
                                DataLabels=false
                            });

                            cModel.SeriesList.Add(new SeriesModel 
                            {
                                SeriesName = dr.Field<string>("platform_name"),
                                CurrentValue = dr.Field<decimal>("play_count"),
                                IsGrowing = dr.Field<Int32>("is_growing") == 1,
                                ChangeRate = (int)dr.Field<decimal>("growing_rate")
                            });
                        }
                    }

                }
                return cModelList;
            }
            catch (Exception ex)
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
