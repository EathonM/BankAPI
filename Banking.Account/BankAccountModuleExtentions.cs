using Amazon;
using Banking.Account.Data;
using Banking.Account.Interfaces;
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
            ConfigurationManager hostConfig,
            ILogger logger,
            List<System.Reflection.Assembly> mediatrAssemblies)
        {

            var moduleConfig = hostConfig.GetSection("Modules:Account");

            string? connectionString = moduleConfig.GetConnectionString(ModuleSettingsConsants.ConnectionString);

            // TODO: Single Settings Services - decrease overhead
            var snsTopicArn = moduleConfig.GetValue<string>(ModuleSettingsConsants.topicARN);
            var snsRegion = moduleConfig.GetValue<string>("AWS:SNS:snsRegion");

            services.AddDbContext<BankAccountDbContext>(options => 
                options.UseSqlServer(connectionString));
           

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<INotificationService, NotificationService>(s => 
            new NotificationService(snsTopicArn!, RegionEndpoint.GetBySystemName(snsRegion)));   
            
            mediatrAssemblies.Add(typeof(BankAccountModuleExtentions).Assembly);

            logger.Information("{Module} module loaded", "BankAccount");
            return services;
        }
    }
}
