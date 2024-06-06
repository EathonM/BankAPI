using Banking.Account.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Application
{
    internal class WithdrawEventStatus
    {
        public string Message { get; set; }
        public EventStatus Status { get; set; }

        public WithdrawEventStatus(string message, EventStatus status)
        {
            Message = message;
            Status = status;
        }
    }
}
