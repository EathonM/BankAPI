using Banking.Account.Domain;
using Banking.Account.Endpoints;
using Banking.Account.UseCases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Application
{
    internal class WithdrawEvent : IRequest<WithdrawEvent>
    {
        public long AccountId { get; }
        public decimal Amount { get; }
        public WithdrawEventStatus status { get; set; }

        public WithdrawEvent(long AccountId, decimal Amount)
        {
            this.AccountId = AccountId;
            this.Amount = Amount;
            status = new WithdrawEventStatus("Withdrawal Pending", EventStatus.Pending);
        }

        public WithdrawEvent(long AccountId, decimal Amount, WithdrawEventStatus status)
        {
            this.AccountId = AccountId;
            this.Amount = Amount;
            this.status = status;
        }

        public bool IsSuccess()
        {
            return this.status.Status == EventStatus.Success;
        }

        public WithdrawEvent SetFailureWithMessage(string message)
        {
            status = new WithdrawEventStatus(message, EventStatus.Failure);
            return this;
        }

        public WithdrawEvent SetSuccessWithMessage(string message)
        {
            status = new WithdrawEventStatus(message, EventStatus.Success);
            return this;
        }
    }
}
