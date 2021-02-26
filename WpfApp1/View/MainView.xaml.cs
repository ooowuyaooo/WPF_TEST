using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Common;
using WpfApp1.ViewModel;
using MoonPdfLib.MuPdf;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            MainViewModel model = new MainViewModel();
            this.DataContext = model;

            model.UserInfo.UserName = GlobalValues.UserInfo.RealName;

            this.WindowState = WindowState.Maximized;
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;

            

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ?
                WindowState.Normal : WindowState.Maximized;
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

            
            LoginView a = new LoginView();
            
            a.ShowDialog();
            FileButton_Click(sender, e);
            FileButton_Click1(sender, e);
            FileButton_Click5(sender, e);
            FileButton_Click6(sender, e);
            FileButton_Click7(sender, e);




        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {


            
            this.Close();

        }
        private void Button_Click5(object sender, RoutedEventArgs e)
        {



            moonPdfPanel.ZoomIn();

        }
        private void Button_Click6(object sender, RoutedEventArgs e)
        {



            moonPdfPanel.ZoomOut();

        }


        private bool _isLoaded = false;

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {



            //string filePath = "F://Csharp_projects/pdfreader/1.pdf";
            string filePath = GlobalValues.UserInfo.Password.ToString();

            try
            {
                moonPdfPanel.OpenFile(filePath);
                _isLoaded = true;
            }
            catch (Exception)
            {
                _isLoaded = false;
                MessageBox.Show("找不到该文件，请问工艺工程师！！");
            }


        }

        private void FileButton_Click1(object sender, RoutedEventArgs e)
        {



            string filePath = GlobalValues.UserInfo.Password1.ToString();

            try
            {
                moonPdfPanel.OpenFile(filePath);
                _isLoaded = true;
            }
            catch (Exception)
            {
                _isLoaded = false;
                MessageBox.Show("找不到该文件，请问工艺工程师！！");
            }


        }

        private void FileButton_Click5(object sender, RoutedEventArgs e)
        {



            string filePath = GlobalValues.UserInfo.Password2.ToString();

            try
            {
                moonPdfPanel.OpenFile(filePath);
                _isLoaded = true;
            }
            catch (Exception)
            {
                _isLoaded = false;
                MessageBox.Show("找不到该文件，请问工艺工程师！！");
            }


        }

        private void FileButton_Click6(object sender, RoutedEventArgs e)
        {



            string filePath = GlobalValues.UserInfo.Password3.ToString();

            try
            {
                moonPdfPanel.OpenFile(filePath);
                _isLoaded = true;
            }
            catch (Exception)
            {
                _isLoaded = false;
                MessageBox.Show("找不到该文件，请问工艺工程师！！");
            }


        }

        private void FileButton_Click7(object sender, RoutedEventArgs e)
        {



            string filePath = GlobalValues.UserInfo.Password4.ToString();

            try
            {
                moonPdfPanel.OpenFile(filePath);
                _isLoaded = true;
            }
            catch (Exception)
            {
                _isLoaded = false;
                MessageBox.Show("找不到该文件，请问工艺工程师！！");
            }


        }
    }
}
