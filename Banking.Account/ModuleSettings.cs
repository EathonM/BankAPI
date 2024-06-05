using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account
{
    public static class ModuleSettingsConsants
    {
        public static string ConnectionString = "Bank.AccountDb";
        public static string topicARN = "AWS:SNS:TopicARN";
        public static string snsRegion = "AWS:SNS:regionEndpoint";
    }
}
