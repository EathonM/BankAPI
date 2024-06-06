using Banking.Account.Application.Interfaces;
using Banking.Account.Application;
using Banking.Account.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.UseCases
{
    internal class WithdrawEventHandler
        : IRequestHandler<WithdrawEvent, WithdrawEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IBankAccountRepository _bankAccountRepository;

        public WithdrawEventHandler(INotificationService notificationService, IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _notificationService = notificationService;
        }

        public async Task<WithdrawEvent> Handle(WithdrawEvent request, CancellationToken cancellationToken)
        {

            var account = await _bankAccountRepository.GetAccountAsync(request.AccountId);

            if (account == null) return request.SetFailureWithMessage("Account not found");
            if (account.Balance < request.Amount) return request.SetFailureWithMessage("Insufficient Funds");

            try
            {
                // try catch to avoid concurrency errors
                account.Balance -= request.Amount;
                await _bankAccountRepository.UpdateBalanceAsync(account);
                request.SetSuccessWithMessage("Withdrawal Successful");
                await _notificationService.NotifyAccountHolder(request, cancellationToken);
            }
            catch 
            {
                return request.SetFailureWithMessage("Withdrawawl could not be processed");
            }

            return request;
        }

       
    }
}
