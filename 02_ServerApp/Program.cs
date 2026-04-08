using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _02_ServerApp
{
    class ChatServer
    {
        const short port = 4040;
        const string JOIN_CMD = "$<join>";
        //127.0.0.1
        UdpClient udpClient ;
        IPEndPoint remoteEP = null;
        List<IPEndPoint> members;
        public ChatServer()
        {
             udpClient = new UdpClient(port);
             members = new List<IPEndPoint>();
        }
        private void AddMember(IPEndPoint member)
        {
            members.Add(remoteEP);
            Console.WriteLine("Member was added!");
        }
        public void Start()
        {
            while (true)
            {
                byte[] data = udpClient.Receive(ref remoteEP);
                string message = Encoding.UTF8.GetString(data);
                Console.WriteLine($"Got message : {message}." +
                    $"From : {remoteEP}. Time : {DateTime.Now.ToShortTimeString()}");
                switch (message)
                {
                    case JOIN_CMD:
                        AddMember(remoteEP);
                        break;
                    default:
                        SendToAllMembers(data);
                        break;
                }
            }
        }
        private void SendToAllMembers(byte[] data)
        {
            foreach (IPEndPoint member in members)
            {
                udpClient.SendAsync(data, data.Length, member);
            }
        }

    }
    internal class Program
    {
       
        static void Main(string[] args)
        {
           ChatServer server = new ChatServer();    
                server.Start();

           
        }
    }
}
