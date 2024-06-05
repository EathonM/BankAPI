using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Banking.Account.UseCases;

namespace Banking.Account.Endpoints;
public class Withdraw(IMediator _sender): Endpoint<WithdrawRequest>
{
    public override void Configure()
    {
        Post(WithdrawRequest.route);
        AllowAnonymous();
    }
    public override async Task HandleAsync(WithdrawRequest request, CancellationToken ct)
    {
        var response = await _sender.Send(new WithdrawCommand(request.AccountId, request.Amount));

        await SendOkAsync(response);
    }
}
