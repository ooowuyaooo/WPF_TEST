using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Common;
using WpfApp1.DataAccess;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class LoginViewModel:NotifyBase
    {
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public CommandBase CloseWindowCommand { get; set; }

        public CommandBase LoginCommand { get; set; }
        public CommandBase LoginCommand1 { get; set; }
        private string _errorMessage;
        


        private Visibility _showProgress;
        public Visibility ShowProgress
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value;
                this.DoNotify();
            }
        }

        private Visibility _showProgress1;
        public Visibility ShowProgress1
        {
            get { return _showProgress1; }
            set
            {
                _showProgress1 = value;
                this.DoNotify();
            }
        }




        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; this.DoNotify(); }
        }


        public LoginViewModel()
        {
            this.ShowProgress = Visibility.Hidden;
            this.ShowProgress1 = Visibility.Hidden;
            //this.LoginModel = new LoginModel();
            //this.LoginModel.UserName = "Jovan";
            //this.LoginModel.Password = "123123";
            this.CloseWindowCommand = new CommandBase();
            this.CloseWindowCommand.DoExcute = new Action<object>((o) => {
                (o as Window).Close();
            });
            this.CloseWindowCommand.DoCanExcute = new Func<object, bool>((o) => { return true; });

            this.LoginCommand = new CommandBase();
            this.LoginCommand.DoExcute = new Action<object>(DoLogin);
            this.LoginCommand.DoCanExcute = new Func<object, bool>((o) => { return true; });

            this.LoginCommand1 = new CommandBase();
            this.LoginCommand1.DoExcute = new Action<object>(DoLogin1);
            this.LoginCommand1.DoCanExcute = new Func<object, bool>((o) => { return true; });

            
        }

        private void DoLogin(object o)
        {
            this.ShowProgress = Visibility.Visible;
            this.ErrorMessage = "查询中.....";
            if (string.IsNullOrEmpty(LoginModel.UserName))
            {
                
                this.ErrorMessage = "找不到产品号码，请联系工艺工程师！！！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
           
            
            //if (string.IsNullOrEmpty(LoginModel.Password))
            //{
            //    this.ErrorMessage = "请输入密码！";
            //    this.ShowProgress = Visibility.Collapsed;
            //    return;
            //}
            //if (string.IsNullOrEmpty(LoginModel.ValidationCode))
            //{
            //    this.ErrorMessage = "请输入验证码！";
            //    this.ShowProgress = Visibility.Collapsed;
            //    return;
            //}
            //if (LoginModel.ValidationCode.ToLower() != "7364")
            //{
            //    this.ErrorMessage = "验证码不正确！";
            //    this.ShowProgress = Visibility.Collapsed;
            //    return;
            //}

            Task.Run(new Action(() => {
            try
            {
                var user = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName);
                if (user == null)
                {
                    throw new Exception("查询失败，检查网络通讯！！");

                }

                GlobalValues.UserInfo = user;

                    this.ErrorMessage = GlobalValues.UserInfo.RealName.ToString();
                    this.ShowProgress1 = Visibility.Visible;

                    


                    //Application.Current.Dispatcher.Invoke(new Action(() =>
                    //    {
                    //        (o as Window).DialogResult = true;
                    //    }));



                }
            catch(Exception ex)
            {
                this.ErrorMessage = ex.Message;
                }
            }));
            

            
        }
        private void DoLogin1(object o)
        {
            
            Task.Run(new Action(() => {
                try
                {

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            (o as Window).DialogResult = true;
                        }));



                }
                catch (Exception ex)
                {
                    this.ErrorMessage = ex.Message;
                }
            }));
        }


    }


}
 