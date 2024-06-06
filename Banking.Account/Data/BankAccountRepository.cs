
using Banking.Account.Interfaces;
using Banking.Account.UseCases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Data
{
    internal class BankAccountRepository: IBankAccountRepository
    {
        private readonly BankAccountDbContext _dbContext;
        public BankAccountRepository(BankAccountDbContext bankAccountDbContext) 
        {
            _dbContext = bankAccountDbContext;
        }

        public async Task<BankAccount> GetAccount(long accountId)
        {
            return await _dbContext.Account.FindAsync(accountId);
        }

        public async Task UpdateBalance(BankAccount account)
        {
            try
            {
                _dbContext.Account.Update(account);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Concurrency exception", ex);
            }
        }   

        public async Task<BankAccount> Deposit(long accountId, decimal amount)
        {
            var account = _dbContext.Account.FindAsync(accountId).Result;
            if (account != null)
            {
                account.Balance += amount;
            }

            await _dbContext.SaveChangesAsync();
            return account;
        }
    }

}
