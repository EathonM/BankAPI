using Banking.Account.Data;
using Banking.Account.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> GetAccount(long accountId);
        Task UpdateBalance(BankAccount account);
        Task<BankAccount> Deposit(long accountId, decimal amount);
    }
}
