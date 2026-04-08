using System.Collections.ObjectModel;
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
        UdpClient udpClient;
        IPEndPoint serverEndPoint;
        const string serverAddress = "127.0.0.1";
        const int server_port = 4040;
        ObservableCollection<MessageInfo> messages = new ObservableCollection<MessageInfo>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = messages;
            udpClient = new UdpClient();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(serverAddress), server_port);
        }

        private  void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            string message = msgTextBox.Text;
            SendMessage(message);
        }

        private void Join_Button_Click(object sender, RoutedEventArgs e)
        {
            string message = "$<join>";
            SendMessage(message);
            Listen();
        }
        private async void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            await udpClient.SendAsync(data, data.Length, serverEndPoint);
        }
        private async void Listen()
        {
            while (true)
            {
                var result = await udpClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                messages.Add(new MessageInfo(message, DateTime.Now));
            }

        }
    }

    class MessageInfo
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public MessageInfo(string Message, DateTime Time)
        {
            this.Message = Message;
            this.Time = Time;
        }
        public override string ToString()
        {
            return $"Message : {Message}. Time: {Time}";
        }

    }
}