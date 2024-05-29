using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
namespace Banking.Account.Endpoints
{
    public class WithdrawRequestValidator: Validator<WithdrawRequest>
    {
        public WithdrawRequestValidator()
        {

            RuleFor(x => x.AccountId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("AccountId is required");

            RuleFor(x => x.Amount)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0");

        }
    }
}
