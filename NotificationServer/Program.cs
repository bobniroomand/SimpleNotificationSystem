using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notification;
using System.Threading;
using Thrift.Transport;
using Thrift.Server;
using Thrift.Protocol;

namespace NotificationServer
{
    class Program
    {
        public static int c = 0;
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((callback) => { RunNotificationService(); }));
            while (true) ;
        }

        static void RunNotificationService()
        {
            var handler = new NotificationHandler();
            var processor = new NotificationService.Processor(handler);

            TServerTransport transport = new TServerSocket(9091);
            TServer server = new TThreadPoolServer(processor, transport);

            Console.WriteLine("start serving...");
            server.Serve();
        }
    }

    class NotificationHandler : NotificationService.Iface
    {
        public string GetNotifications()
        {
            var notifications = NotificationHelper.Instance.GetNotificationsAsync(0);
            Console.WriteLine("Req#{0}, Notification:{1}", ++Program.c, notifications.Result);
            return notifications.Result;
        }
    }
}
