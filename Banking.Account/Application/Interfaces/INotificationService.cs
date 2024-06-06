using Banking.Account.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Application
{
    internal interface INotificationService
    {
        Task NotifyAccountHolder(WithdrawEvent notification, CancellationToken ct);
    }
}
