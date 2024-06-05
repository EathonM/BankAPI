using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Interfaces
{
    public interface INotificationService
    {
        Task NotifyAccountHolder(string message, CancellationToken ct);

    }
}
