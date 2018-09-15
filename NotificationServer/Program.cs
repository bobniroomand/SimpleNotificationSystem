using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notification;
using System.Threading;
using Thrift.Transport;
using Thrift.Server;

namespace NotificationServer
{
    class Program
    {
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

            server.Serve();
        }
    }

    class NotificationHandler : NotificationService.Iface
    {
        public string GetNotifications()
        {
            return NotificationHelper.Instance.GetNotifications(0);
        }
    }
}
