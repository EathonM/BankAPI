using Banking.Account.Data;
using Banking.Account.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.UseCases
{
    internal class WithdrawCommandHandler
        :IRequestHandler<WithdrawCommand, WithdrawCommandResult>
    {
        private readonly INotificationService _notificationService;
        private readonly IBankAccountRepository _bankAccountRepository;

        public WithdrawCommandHandler(INotificationService notificationService, IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _notificationService = notificationService;
        }

        public async Task<WithdrawCommandResult> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var account = await _bankAccountRepository.GetAccount(request.AccountId);

            if (account == null) return new WithdrawCommandResult(false, "Account not found");
            
            if (account.Balance < request.Amount) return new WithdrawCommandResult(false, "Insufficient Funds");

            account.Balance -= request.Amount;

            await _bankAccountRepository.UpdateBalance(account);

            await _notificationService.NotifyAccountHolder($"You have withdrawn {request.Amount}", cancellationToken);

            return new WithdrawCommandResult(true, "Withdrawal Successful");
        }
    }
}
