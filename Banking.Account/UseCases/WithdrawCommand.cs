using Banking.Account.Endpoints;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.UseCases
{
    internal record WithdrawCommand(long AccountId, decimal Amount): IRequest<WithdrawResponse>;
}
