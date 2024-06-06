using Banking.Account.Domain.Entities;
using Banking.Account.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Application.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> GetAccountAsync(long accountId);
        Task UpdateBalanceAsync(BankAccount account);
        Task<BankAccount> Deposit(long accountId, decimal amount);
    }
}
