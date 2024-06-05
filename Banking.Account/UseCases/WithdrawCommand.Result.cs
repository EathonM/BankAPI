using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.UseCases
{
    internal record WithdrawCommandResult(bool Success, string Message);
}
