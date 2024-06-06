using Banking.Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Infrastructure.Data
{
    internal class BankAccountDbContext : DbContext
    {
        public BankAccountDbContext(DbContextOptions<BankAccountDbContext> options)
            : base(options)
        {
        }

        public DbSet<BankAccount> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankAccountDbContext).Assembly);

            modelBuilder.Entity<BankAccount>(ConfigureBankAccount);

            base.OnModelCreating(modelBuilder);

        }

        private void ConfigureBankAccount(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.AccountId);
            builder.Property(x => x.AccountId).ValueGeneratedOnAdd();    // assuming no foreign key reference to an Account Entity ?
            builder.Property(x => x.Balance).HasColumnType("decimal(18, 2)").IsRequired();
        }
    }
}
