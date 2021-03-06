## Table of contents
* [Info](#info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Extra info](#extra-info)

## Info
This project is a sample for wrapping all the logic for ASP.NET Identity in a couple of different DDLs and moving it out of the ASP.NET MVC or Web API app, you just need to call 
the services in the service layer of this project.
The project contains 3 layers plus a library for common utils and things -> **[Infrastructure](https://github.com/ArashSasani/IdentityWrapper/tree/master/WebApplication.Infrastructure).**

Each layer references the appropriate layer to show its modular architecture, something similar to *3-layer architecture*.

* *The core layer* is resposible for holding the domain models which you will use them directly for mapping to your tables using EF code first in your database context or in case your project is DDD your main context aka uber context.
* *The data layer* is mainly responsible for db migrations using nuget package manager console, Identity db context, initial data to be seeded or not! and Identity configs which you should call 
in your app startup or global.asax according to your web app structure.
* *The service layer* is holding Data transfer objects aka DTO(s) which will be the objects that you want to pass to the UI (you don't want to pass models directly! check stackoverflow 
for the reasons!), it also contains [Automapper](https://automapper.org/) profile setup which should be called also on the app startup, please check how to, in the Automapper docs.
Both data layer and service layer contain interfaces for repositories and application services, you can register them using your favorite DI tool.

## Technologies
Main packages used in the project:
* Entity Framework 6.4
* ASP.NET Identity 2.2 and its dependencies

## Setup
To use this project, **reference all its DDLs** in your project and just call services in the service layer according to your needs, **build and restore the packages** and you should be good to go.
Most methods are in the **[AuthService](https://github.com/ArashSasani/IdentityWrapper/blob/master/CMS.Service/Services/AuthService.cs)** which you can use for authentication and authorization in your web app.
In your web app you can use the default **Authorize attribute** for your controllers and actions which will bind automatically to **ASP.NET Identity Authentication and authorization** or implement 
your own logic for role-based, claim-based, etc authorization.

## Extra info
for this sample I used role-based authorization with [OAuth](https://oauth.net/) opaque token in my web api app, I just initialized different paths or routes for the app as the 
access paths, Then each user can have access either to some or all of them,
then based on the authorized access path the user can either get in the route or get 401 unauthorize for the route.
You can also use jwt or any other 3rd party token based authentication and authorizan system.
