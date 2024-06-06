using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Banking.Account.UseCases;
using Serilog;
using Banking.Account.Application;

namespace Banking.Account.Endpoints;
public class Withdraw(IMediator _sender): Endpoint<WithdrawRequest>
{    
    public override void Configure()
    {
        Post(WithdrawRequest.route);
        AllowAnonymous();

        // TODO: Add Authentication Policy  
    }
    public override async Task HandleAsync(WithdrawRequest request, CancellationToken ct)
    {
        var response = await _sender.Send(new WithdrawEvent(request.AccountId, request.Amount));

        if (response.IsSuccess())
        {
            await SendOkAsync(response);
        }
        else await SendAsync(response, 400);
    }
}
