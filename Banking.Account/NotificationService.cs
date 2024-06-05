using Amazon.SimpleNotificationService;
using Banking.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account
{
    internal abstract class NotificationService : INotificationService
    {
        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly string _topicArn = "arn:aws:sns:YOUR_REGION:YOUR_ACCOUNT_ID:YOUR_TOPIC_NAME";

        public NotificationService() { }

        public async Task NotifyAccountHolder(string message, CancellationToken ct) => await _snsClient.PublishAsync(_topicArn, message, ct);
    }

    
}
