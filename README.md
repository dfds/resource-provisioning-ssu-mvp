ResourceProvisioning proof-of-concept
======================================

This repository contains the complete source code for the ResourceProvisioning proof-of-concept and serves as a starting point for .NET Core developers looking to onboard the resource provisioning bandwagon. While the sample code is provided "AS-IS" it is possible to reach out to the trainer for any questions/comments.

## Are you here for something besides the source code?

In addition to providing the source code, this repository also acts as a useful nexus for things related to the Microservices:

 * Want to **learn more** about Microservice Architectures? See the [related information](#user-content-related-information) section.

## Getting started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Before installing the solution on your local developer machine you need to ensure that the following tools are installed and configured:

* [Powershell (>= 4.x)](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-6)
* [Visual Studio 2017](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2017) or [Visual Studio Code](https://code.visualstudio.com/download)
* [.NET Core 2.2](https://dotnet.microsoft.com/download)

### Installing

TODO

## Dependencies

The following is non-exhustive list of the various dependencies that is utilized in the OMSK repository:

* [AutoMapper](https://automapper.readthedocs.io/en/latest/Getting-started.html)
* [Mediatr](https://github.com/jbogard/MediatR/wiki)
* [Swashbuckle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio#add-and-configure-swagger-middleware)
* [Polly](http://www.thepollyproject.org/)

## Contributing
Please read [CONTRIBUTING.md](./docs/CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Tobias Andersen** - *Initial work*
* **Kim Lindhard** - *Initial work*
* **Emil H.** - *Initial work*

## Related information

This section contains a list of curated self learning resources that serves as a primer for  IT professionals that want to familiarize themselves with the Microservice paradigme as well as some of the techniques they can leverage in order to successfully implement scalable/containerized microservices.

### Microservice architecture

 * [What is a Microservice?](https://www.youtube.com/watch?v=wgdBVIX9ifA)
 * [Designing Microservice architectures](https://www.youtube.com/watch?v=j6ow-UemzBc&t=468s)
 * [Event driven architecture](https://www.youtube.com/watch?v=STKCRSUsyP0)
 * [Microservices + Events + Docker = A Perfect Trio](https://www.youtube.com/watch?v=sSm2dRarhPo)

### Microservice patterns
 * [API Gateway](https://microservices.io/patterns/apigateway.html)
 * [Access token](https://microservices.io/patterns/security/access-token.html)
 * [Application events](https://microservices.io/patterns/data/application-events.html)
 * [Database per service](https://microservices.io/patterns/data/database-per-service.html)
 * [Health check API](https://microservices.io/patterns/observability/health-check-api.html)
 * [CQRS](https://microservices.io/patterns/data/cqrs.html)
 * [DDD - Aggregates](https://deviq.com/aggregate-pattern/)
 * [DDD - Domain events](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/domain-events-design-implementation)

### Microservice development

 * [Explicit architecture](https://herbertograca.com/tag/explicit-architecture/)
 * [XP from the trenches](https://koukia.ca/a-microservices-implementation-journey-part-1-9f6471fe917)
* [Tackle Business Complexity in a Microservice with DDD and CQRS](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/)
 * [Implement the infrastructure persistence layer with Entity Framework Core](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implemenation-entity-framework-core)
 * [Domain events vs Integration events](https://blogs.msdn.microsoft.com/cesardelatorre/2017/02/07/domain-events-vs-integration-events-in-domain-driven-design-and-microservices-architectures/)
 * [Background tasks in microservices](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/background-tasks-with-ihostedservice)
 * [Health checks in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2)
 * [Custom claims in Azure AD](https://devonblog.com/cloud/azure-ad-adding-employeeid-claims-in-azure-ad-jwt-token/)

### Microservice samples

 * [Microservices Architecture and Containers based Reference Application](https://github.com/dotnet-architecture/eShopOnContainers)
 * [Microservices by example with DevMentors](https://dev.to/rafalpienkowski/microservices-by-example-with-devmentors-505m)