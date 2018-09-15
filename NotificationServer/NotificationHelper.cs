using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationServer
{
    public class NotificationHelper
    {
        private static NotificationHelper instance = null;
        private static readonly object padlock = new object();
        AutoResetEvent notifEvent = new AutoResetEvent(false);
        NotificationHelper() { }

        private static object client;
        public static NotificationHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NotificationHelper();
                        //client = new
                        //{
                        //    Name = "john doe"
                        //};
                    }
                    return instance;
                }
            }
        }

        public string GetNotifications(int Id)
        {
            bool hasNotification = HasNotification(Id);
            
            if (!hasNotification)
            {
                
                //var client = GetClient(Id);
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(10000);
                    //Monitor.Pulse(client);
                    notifEvent.Set();
                });
                //wait for Notifications...
                //Monitor.Wait(client);
                notifEvent.WaitOne();
            }
            string notifications = SearchForNotifications(Id);
            return notifications;
        }

        private bool HasNotification(int id)
        {
            Random random = new Random();
            int isThereAnyNotifs = random.Next(0, 2);

            return isThereAnyNotifs != 0;
        }

        private object GetClient(int id)
        {
            return client;
        }

        private string SearchForNotifications(int id)
        {
            return "foo";
        }
    }
}
