## Table of contents
* [Info](#info)
* [Technologies](#technologies)
* [Setup](#setup)

## Info
This project is a sample for wrapping all the logic for ASP.NET Identity in a couple of different DDLs and moving it out of the ASP.NET MVC or Web API app, you just need to call 
the services in the service layer of this project.
The project contains 3 layers plus a library for common utils and things -> **[Infrastructure](https://github.com/ArashSasani/IdentityWrapper/tree/master/WebApplication.Infrastructure).**
Each layer refernces the appropriate layer to show its modular architecture, something similar to *3-layer architecture*.
*The core layer* is resposible for holding the domain models which you will use them directly in your database context or in case your project is DDD your main context aka uber context.
*The data layer* is mainly responsible for db migration using nuget package manager, Identity db context, initial data to be seeded or not! and Identity configs which you should call 
on your app startup or global.asax according to your web app structure.
*The service layer* is holding Data transfer objects aka DTO(s) which will be the object that you want to pass to the UI (you don't want to pass models directly! check stackoverflow 
for the reasons!), it also contains [Automapper](https://automapper.org/) profile which should be called also on the app startup, please check how to in the Automapper docs.
Both data layer and service layer contain interfaces for repositories and application services, you can register them using your favorite DI tool.

## Technologies
Main packages used in the project:
* Entity Framework 6.4
* ASP.NET Identity 2.2 and its dependencies

## Setup
To use this project, **reference all its DDLs** in your project and just call services in the service layer according to your needs, **build and restore the packages** and you are good to go.
Most method are in the **AuthService** which you can use for authentication and authorization in your web app.
In your web app you can the default **Authorize attribute** for your controllers and actions which will be bind automatically with **ASP.NET Identity Authentication and authorization** or implement 
your own logic for role-based, claim-based, etc authorization.
