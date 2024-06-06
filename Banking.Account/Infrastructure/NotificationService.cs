using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Banking.Account.Application.Interfaces;
using Banking.Account.Application;
using Banking.Account.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Infrastructure
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

        public async Task NotifyAccountHolder(WithdrawEvent notification, CancellationToken ct) =>
            await _snsClient.PublishAsync(new PublishRequest(_topicArn, notification.status.Message));
    }
}
