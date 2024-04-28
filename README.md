
# MinimalApi
Proof Of Concept - "At Scale"

A place I'm testing if ASP.NET Core minimal APIs are capable of replacing my existing larger controller driven WebAPI's and how a project structure would look

## Dependencies
This repo depends on the latest <em><strong>.NET Core 8</strong></em> and the usual blend of libraries...

- Automapper > for object mapping
- EntityFrameworkCore > grabbing data
- Swagger (OpenAI)  > API Documentation Gen

## Approach

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

> see System.ComponentModel.DataAnnotations Namespace
 
A declarative approach to model validation

## Generic Repositories

Any new entity can be added to the system without implementing a specific repository implementation via use of GenericRepository<<T>> wich provides all the usual CRUD Operations out of the box. Just add a route and corresponding DTO object.

## Child Entities / Nested complex objects

If Generic repository is not enough I added specific Repository implementations for some entities to allow an expansion of an entity.

simply append the query string
> ?expand=true

Repositories that implement complex objects introduce scale / performance issues and although I have included the expand feature these operations should be individually isolated out into other dedicated nanoservices eg 'Azure Functions' for granular pricing and scale reasons.




