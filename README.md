
# MinimalApi
Proof Of Concept - "At Scale"

A place I'm testing if ASP.NET Core minimal APIs are capable of replacing my existing larger controller driven WebAPI's and how a project structure would look
Pls update to use latest <em><strong>.NET Core 8</strong></em> or greater

## Local Dev ( Connected Services )

- SQLServer
- Azurite emulator - Azure Blog Storage

> https://www.docker.com/products/docker-desktop/
> docker pull mcr.microsoft.com/azure-sql-edge
>docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=MinApiPass1" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sql mcr.microsoft.com/azure-sql-edge

NOTE: user and pass from appsettings.json 

### --- Azurite emulator ---

> docker pull mcr.microsoft.com/azure-storage/azurite
> docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 mcr.microsoft.com/azure-storage/azurite

## :) RUN ME :)

 /Alex.MinimalApi.Service folder

>dotnet build
>dotnet ef database update
>dotnet run

 / root folder

>dotnet test

VIEW API DOCS

> http://localhost:5058/swagger/ to see API Documentation

MAKE REQUESTS
see 'requests.http' file sample requests you can execute
e.g.   http://localhost:5058/Employee



## Packages Dependencies ?

- Automapper > for object mapping
- EntityFrameworkCore > grabbing data
- Swagger (OpenAI)  > API Documentation Gen
- Moq > aid MSTest unit tests


## Design

A bunch of __'Rest Endpoints'__ to expose an __'SQLServer model'__ via a mostly __'CRUD'__ transactional method. 

__[Presentation]__

declare Http routes, model validation, OpenAPI document description
  
__[Core]__

DDD Business domain only logic
   
__[Repository]__

Data access via repository pattern

> NOTE 'Core' is an empty abstraction layer for the DDD Domain model.
  this can be removed if no domain model rules/logic( see Domain Driven Design)  will be implemented.
  i.e. if API is to be consumed in event driven architecture and workflow & business rules are managed outside of API.

## Model Validation
Due to current limitations with minimalAPi I use manual model validation using FluentValidation as recommended by Microsoft. NOTE: This is likely to change in future 
> see https://www.nuget.org/packages/FluentValidation/
 
 I would like to see a declarative implemtation added like System.ComponentModel.DataAnnotations that we all used in MVC style apps.

## Generic Repositories

Any new entity can be added to the system without implementing a specific repository implementation via use of GenericRepository<<T>> wich provides all the usual CRUD Operations out of the box. Just add a route and corresponding DTO object.





