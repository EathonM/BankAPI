using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Settings
{
    public static class ModuleSettingsConsants
    {
        public static string ConnectionString = "Bank.AccountDb";
        public static string snsAccountId = "AWS:SNS:YOUR_ACCOUNT_ID";
        public static string snsTopicName = "AWS:SNS:YOUR_TOPIC_NAME";
        public static string snsRegionEndpoint = "AWS:SNS:REGION_ENDPOINT";
    }
}
