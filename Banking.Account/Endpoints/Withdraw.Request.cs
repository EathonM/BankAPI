using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Endpoints
{
    public class WithdrawRequest
    {
        public const string route = "/withdraw";
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
