using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Common;

namespace WpfApp1.ViewModel
{
    public class LoginViewModel
    {
        public CommandBase CloseWindowCommand { get; set; }

        public LoginViewModel()
        {
            this.CloseWindowCommand = new CommandBase();
            this.CloseWindowCommand.DoExcute = new Action<object>((o) => {
                (o as Window).Close();
            });
            this.CloseWindowCommand.DoCanExcute = new Func<object, bool>((o) => { return true; });
        }
    }


}
 