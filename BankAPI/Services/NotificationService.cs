using Amazon.SimpleNotificationService;
using Banking.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Services
{
    internal abstract class NotificationService
    {
        private readonly IAmazonSimpleNotificationService _snsClient;
        public NotificationService() { }

        public virtual async Task QueueNotification(string message)
        {
            _snsClient.Not
            Console.WriteLine(message);
        }
    }

    
}
