using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace _03_SMTP_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string server = "smtp.gmail.com";
        int port = 587;
        string username = "lenailyshun@gmail.com";
        string password = "hwpmnsbvhaqkgfcd";
        //03_SMTP_Client
        public MainWindow()
        {
            InitializeComponent();
            fromTb.Text = "lenailyshun@gmail.com";
            toTb.Text = "teyes23644@mypethealh.com";
        }
        public MainWindow(string login)
        {
            InitializeComponent();
            fromTb.Text = login;
            toTb.Text = "teyes23644@mypethealh.com";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MailMessage message = new MailMessage(fromTb.Text,
                toTb.Text, subjectTb.Text, messageTb.Text );

            using (StreamReader sr = new StreamReader(@"Files/mail.html"))
            {
                message.Body = sr.ReadToEnd();  
            }
            message.IsBodyHtml = true;  
            message.Priority = MailPriority.High;
            message.Attachments.Add(new Attachment(@"Files/text.txt"));
            message.Attachments.Add(new Attachment(@"Files/nuts.jpg"));

            SmtpClient smtpClient = new SmtpClient(server,port);
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential(username, password);

            smtpClient.SendCompleted += SmtpClient_SendCompleted;
            smtpClient.SendAsync(message, message);

        }

        private void SmtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var state = e.UserState as MailMessage;
            MessageBox.Show($"Message was send ! {state?.Subject}");
        }
    }
}