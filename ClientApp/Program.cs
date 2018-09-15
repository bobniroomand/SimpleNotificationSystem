using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 200; i++)
            {
                int c = i;
                Thread t = new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = false;
                                var client = OpenConnectionToServer();
                                var notifs = client.GetNotifications();
                                Console.WriteLine("notifs of client #{0}: {1}",c ,notifs);
                            });
                t.Start();
            }

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
