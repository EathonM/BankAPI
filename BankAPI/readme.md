## This API rewrites the funtionality found in the BankAccountEndpoints - BankAccountController
## Architecture
The achitecture is that of a Modular Monolith.
This architecture allows for any module that is registered in the Host API to be scaled out independently as a microservice
should the need arise.
Each module is written standalone, meaning no dependency on another module.
Modules are designed to be small scalable pieces of the overall API. The intention is to anticipate 
potential increases in load/request to a single module, and have the flexibility of deploying it as a standalone
microservice (i.e. AWS Fargate, Lambda, or On-Prem) when the need arises.

# Inter-Module Comms
Modules own their schema and data. No schema is referenced outside of the module i.e. the BankingAccount Schema exists only within
the Banking.Account module.
Requests can be made to the module via the mediator, and the module will return the response.

Once scaled out, modules can communicate via a message broker (AWS SQS) or a message bus (RabbitMQ).

