### Coding standard and Clean Code principles

This module follows DDD principles with some focus on Vertical Slice Architecture.

## Layers
# Presentation
1. REPR Endpoints
The user entry point of this module is the Endpoints. 
These are REPR -Requests Per Response endpoints.
The allows for code segregation and context managements - for each endpoint file, the context is singular i.e. the
purpose of that particular endpoint.
This allows integration tests to be written in a file-per-endpoint fashion as well. Again, this enhances readibility 
and maintainability.
Furthermore, this approach forms the foundation for single LAMDA functions per endpoint - minimal work is required to extract 
an endpoint into a scalable AWS Lambda function.

The REPR pattern allows code to shipped in slices, allows for higher deployment frequency and reduces the risk of breaking changes.


2. Contracts
Endpoint contacts are housed behind the endpoint itself by naming convention. This is simply for the purpose of readibility
i.e. Withdaw => Withdraw.Request => Withdraw.Response

# Application - Use Cases
1. Use cases are contracts or records that are referenced by handlers to execute business logic.
Here the application layer follows the CQRS pattern. Again, making use of the Mediator pattern allows the module to adhere to CQRS almost intuitively.

2. Handlers are an extension of the Mediator pattern (and library in this case).																																													
Handler will handle any request (command or query) and execute the appropriate use case.

Handlers reference repositories and interna services via DI, while external modules are referenced via mediatr.
3. Interfaces are shared between the application and domain layers. This is to ensure that the domain layer is not dependent on the application layer.
# Domain
Domain exists within the Data folder.
This is where Dto's can be defined alongside Schema entities. 
The domain has no reference to any other layer - following DDD principles (losely).

# Infrastructure
BankAccountModuleExtensions is the registration entry point for the module.
Infrastructure is injected here as needed, as well as core/shared services that are maintained the host API.
The Banking.Account module is bootstrapped using mediatr.







