using Alex.MinimalApi.Service.Core;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Presentation
{
    /// <summary>
    /// Route handler for '/Employee'  route
    /// </summary>
    public class EmployeeRouteHandler : IRouteHandler
    {
        public static void CreateRoutes(WebApplication app)
        {
            app.MapGet("/Employee",
                (bool? expand, IMapper mapper, IEmployeeRepository repo) => ListEmployee(expand, mapper, repo))
                 .Produces<List<Pres.Employee>>(StatusCodes.Status200OK)
                .WithOpenApi(operation => new(operation)
                {
                    OperationId = "list-employee",
                    Summary = "List Employees",
                    Tags = new List<OpenApiTag>() { new() { Name = "Employee" } }
                });

            app.MapGet("/Employee/{id}",
                (int id, IMapper mapper, IEmployeeRepository repo) => GetEmployeeById(id, mapper, repo))
                .Produces<Pres.Employee>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi(operation => new(operation)
                {
                    OperationId = "get-employee-by-id",
                    Summary = "Get Employee by id",
                    Tags = new List<OpenApiTag>() { new() { Name = "Employee" } }
                });

            app.MapPost("/Employee",
                (Pres.Employee emp, IMapper mapper, IEmployeeRepository repo) => CreateEmployee(emp, mapper, repo))
                .Produces<Pres.Employee>(StatusCodes.Status201Created)
                .WithOpenApi(operation => new(operation)
                {
                    OperationId = "create-employee",
                    Summary = "Create Employee",
                    Tags = new List<OpenApiTag>() { new() { Name = "Employee" } }
                });



        }

        #region Delegates

        static async Task<IResult> ListEmployee(bool? expand, IMapper mapper, IEmployeeRepository repo)
        {
            List<Core.Employee> result = await repo.ListAsync(expand == null ? false : expand.Value);
            List<Pres.Employee> output = mapper.Map<List<Pres.Employee>>(result);
            return Results.Ok(output);
        }

        static async Task<IResult> GetEmployeeById(int id, IMapper mapper, IEmployeeRepository repo)
        {
            var result = await repo.GetAsync(id);
            Pres.Employee output = null;
            if (result != null)
            {
                output = mapper.Map<Pres.Employee>(result);
                return Results.Ok(output);
            }
            return Results.NotFound(id);

        }

        static async Task<IResult> CreateEmployee(Pres.Employee employee, IMapper mapper, IEmployeeRepository repo)
        {
            Core.Employee input = mapper.Map<Core.Employee>(employee);
            var result = await repo.CreateAsync(input);
            Pres.Employee output = mapper.Map<Pres.Employee>(result);
            return Results.Created($"/Employee/{result.Id}", output);
        }

        #endregion

    }
}
