using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = OpenConnectionToServer();
            var notifs = client.GetNotifications();
            Console.WriteLine(notifs);
            
        }
        static NotificationService.Client OpenConnectionToServer()
        {
            var transport = new TSocket("localhost", 9091);
            var protocol = new TBinaryProtocol(transport);
            var client = new NotificationService.Client(protocol);
            
            transport.Open();
            return client;
        }
    }
}
