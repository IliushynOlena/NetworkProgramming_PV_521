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
using System.Windows.Shapes;

namespace _03_SMTP_Client
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string Login { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login = loginTb.Text;   
            MainWindow mainWindow = new MainWindow(Login);
            //mainWindow.Show();
            //mainWindow.ShowDialog();
            Application.Current.MainWindow = mainWindow; 
            mainWindow.Show();

            this.Close();
        }
    }
}
