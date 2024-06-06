using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Banking.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account
{
    internal class NotificationService : INotificationService
    {
        private readonly IAmazonSimpleNotificationService _snsClient;
        private string _topicArn;

        public NotificationService(string topicARN, RegionEndpoint endpoint) 
        {

            _topicArn = topicARN;
            _snsClient = new AmazonSimpleNotificationServiceClient(endpoint);
        }

        public async Task NotifyAccountHolder(string message, CancellationToken ct) => 
            await _snsClient.PublishAsync(new PublishRequest(_topicArn, message));
    }    
}
