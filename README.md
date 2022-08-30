# :warning: Repository not maintained :warning:

Please note that this repository is currently archived, and is no longer being maintained.

- It may contain code, or reference dependencies, with known vulnerabilities
- It may contain out-dated advice, how-to's or other forms of documentation

The contents might still serve as a source of inspiration, but please review any contents before reusing elsewhere.

ResourceProvisioning v. 0.1
======================================

This repository contains the complete source code for the ResourceProvisioning MVP and serves as a starting point for .NET Core developers looking to onboard the resource provisioning bandwagon. While the sample code is provided "AS-IS" it is possible to reach out to DevEx with any questions/comments or create an issue on GitHub.

## Getting started

The project is split up in quite a few sub-projects that can be found in the "src" directory.

* ResourceProvisioning.Abstractions
* ResourceProvisioning.Broker.Domain
* ResourceProvisioning.Broker.Infrastructure
* ResourceProvisioning.Broker.Application
* ResourceProvisioning.Broker.Host.Api
* ResourceProvisioning.Broker.Host.Worker
* ResourceProvisioning.Broker.AcceptanceTests
* ResourceProvisioning.Cli.Domain
* ResourceProvisioning.Cli.Infrastructure
* ResourceProvisioning.Cli.Application
* ResourceProvisioning.Cli.Host.Console

Of those the following are applications that can be built to an executable:

* ResourceProvisioning.Broker.Host.Api
* ResourceProvisioning.Cli.Host.Console


### Prerequisites

* .Net Core 3.1 SDK - https://dotnet.microsoft.com/download/dotnet-core/3.1
* Powershell Core - https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7
* Docker - https://www.docker.com/


*If you want to release(**Optional**)*
* GitHub Hub - https://hub.github.com/

#### For database migrations
* Dotnet EF CLI - dotnet tool install --global dotnet-ef
### Installing

* **ResourceProvisioning.Broker.Host.Api**

  From the root of the source repo run the following docker commands:

  `docker build -t local-development -f ./src/ResourceProvisioning.Broker.Host.Api/Dockerfile .`
  `docker run -it -p 50900:50900 local-development`

* **ResourceProvisioning.Cli.Application**

  Within the following path "pipeline/ssucli", run the following Powershell script like so:
  
  `pwsh build.ps1 OS_TARGET APP_VERSION`

  Replace *OS_TARGET* with a value supported by the .NET Core SDK runtime targets. See [here](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog#using-rids) for more information on runtime targets.

  Replace *APP_VERSION* with a value that follow semver, e.g. `0.1.0`.

  Thus if you were to build this for a Windows platform that runs 64-Bit, with the version 0.1.0, it would look like this:

  `pwsh build.ps1 win10-x64 0.1.0`
  

## Dependencies

The following is non-exhustive list of the various third-party dependencies that is utilized in the repository:

* [AutoMapper](https://automapper.readthedocs.io/en/latest/Getting-started.html)
* [Mediatr](https://github.com/jbogard/MediatR/wiki)
* [Swashbuckle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio#add-and-configure-swagger-middleware)

## Contributing
Please read [CONTRIBUTING.md](./docs/CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Emil H.** - *Omnistack-developer-god*
* **Kim Lindhard** - *DDD-champion-and-quality-assurance-expert*
* **Tobias Andersen** - *Lazy-guy-on-the-couch*

## Related information

* [Microservice Architecture and Design Patterns for Microservices](https://medium.com/@madhukaudantha/microservice-architecture-and-design-patterns-for-microservices-e0e5013fd58a)
* [Desired state versus actual state in distributed systems](https://downey.io/blog/desired-state-vs-actual-state-in-kubernetes/)
* [Kubernetes Patterns : The Stateful Service Pattern](https://www.magalix.com/blog/kubernetes-patterns-the-stateful-service-pattern)

