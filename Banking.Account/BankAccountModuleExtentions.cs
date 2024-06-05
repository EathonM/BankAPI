using Banking.Account.Data;
using Banking.Account.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account
{
    public static class BankAccountModuleExtentions
    {
        public static IServiceCollection AddBankAccountModule(
            this IServiceCollection services,
            ConfigurationManager config,
            ILogger logger,
            List<System.Reflection.Assembly> mediatrAssemblies)
        {
            var connectionString = config.GetConnectionString("Bank.AccountDb");
            services.AddDbContext<BankAccountDbContext>(config => 
                config.UseSqlServer(connectionString));

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<INotificationService, NotificationService>();   
            
            mediatrAssemblies.Add(typeof(BankAccountModuleExtentions).Assembly);

            logger.Information("{Module} module loaded", "BankAccount");
            return services;
        }
    }
}
