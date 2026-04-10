using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _02_MultyChatUDP_Protocol
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient tcpClient ;
        IPEndPoint serverEndPoint;
        const string serverAddress = "127.0.0.1";
        const int server_port = 4040;
        NetworkStream ns = null;
        StreamReader sr = null;
        StreamWriter sw = null;
        ObservableCollection<MessageInfo> messages = new ObservableCollection<MessageInfo>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = messages;
            tcpClient = new TcpClient();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(serverAddress), server_port);
        }

        private  void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            string message = msgTextBox.Text;
            SendMessage(message);
        }

        private void Connection_Button_Click(object sender, RoutedEventArgs e)
        {
            string message = "$<join>";
            try
            {
                tcpClient.Connect(serverEndPoint);
                ns = tcpClient.GetStream();
                sr = new StreamReader(ns);
                sw = new StreamWriter(ns);
                SendMessage(message);
                Listen();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }
        private void SendMessage(string message)
        {         
           sw.WriteLine(message);
           sw.Flush();

        }
        private async void Listen()
        {
         
            
            while (true)
            {
                string? message = await sr.ReadLineAsync();              
                messages.Add(new MessageInfo(message, DateTime.Now));
            }
        }

        private void Disconnect_Button_Click(object sender, RoutedEventArgs e)
        {
            ns.Close();
            tcpClient.Close();
        }
    }

    class MessageInfo
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public MessageInfo(string? Message, DateTime Time)
        {
            this.Message = Message ?? "";
            this.Time = Time;
        }
        public override string ToString()
        {
            return $"Message : {Message}. Time: {Time}";
        }

    }
}