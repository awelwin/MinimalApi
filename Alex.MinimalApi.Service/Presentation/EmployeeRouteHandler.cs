﻿using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Presentation.Validators;
using AutoMapper;
using FluentValidation;
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
            //Route
            app.MapGet("/Employee",
                (bool? expand,
                IMapper mapper,
                IEmployeeRepository repo,
                IValidator<Pres.Employee> validator) => ListEmployee(expand, mapper, repo))

                //Documentation
                .Produces<List<Pres.Employee>>(StatusCodes.Status200OK)
                .WithOpenApi(op =>
                {
                    op.OperationId = "list-employee";
                    op.Summary = "List Employees";
                    op.Parameters[0].Description = "Expand or inflate Employee objects to contain its child dependencies";
                    op.Responses["200"].Description = "Array of Employees currently in system.";
                    op.Tags = new List<OpenApiTag>() { new() { Name = "Employee" } };
                    return op;
                });

            //Route
            app.MapGet("/Employee/{id}",
                (int id, IMapper mapper, IEmployeeRepository repo) => GetEmployeeById(id, mapper, repo))

                //Documentation
                .Produces<Pres.Employee>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi(op =>
                {
                    op.OperationId = "get-employee-by-id";
                    op.Summary = "Get Employee by id";
                    op.Parameters[0].Description = "Unique Employee Id";
                    op.Responses["200"].Description = "Employee matching id parameter";
                    op.Tags = new List<OpenApiTag>() { new() { Name = "Employee" } };
                    return op;
                });

            //Route
            app.MapPost("/Employee",
                (Pres.Employee emp, IMapper mapper, IEmployeeRepository repo) => CreateEmployee(emp, mapper, repo))

                //validation
                .AddEndpointFilter<ValidationEndpointFilter<Pres.Employee>>()

                //Documentation
                .Produces<Pres.Employee>(StatusCodes.Status201Created)
                .WithOpenApi(op =>
                {
                    op.OperationId = "create-employee";
                    op.Summary = "Create Employee";
                    op.Responses["201"].Description = "Newly created Employee entity with unique Id";
                    op.Tags = new List<OpenApiTag>() { new() { Name = "Employee" } };
                    return op;
                });
        }

        #region Delegates

        public static async Task<IResult> ListEmployee(bool? expand, IMapper mapper, IEmployeeRepository repo)
        {
            List<Core.Employee> result = await repo.ListAsync(expand == null ? false : expand.Value);
            List<Pres.Employee> output = mapper.Map<List<Pres.Employee>>(result);
            return Results.Ok(output);
        }

        public static async Task<IResult> GetEmployeeById(int id, IMapper mapper, IRepository<Core.Employee> repo)
        {
            var result = await repo.GetAsync(id);
            Pres.Employee? output = null;
            if (result != null)
            {
                output = mapper.Map<Pres.Employee>(result);
                return Results.Ok(output);
            }
            return Results.NotFound(id);

        }

        public static async Task<IResult> CreateEmployee(Pres.Employee employee, IMapper mapper, IRepository<Core.Employee> repo)
        {
            if (employee == null)
                return Results.BadRequest();

            Core.Employee input = mapper.Map<Core.Employee>(employee);
            var result = await repo.CreateAsync(input);
            Pres.Employee output = mapper.Map<Pres.Employee>(result);
            return Results.Created($"/Employee/{result.Id}", output);
        }

        #endregion

    }
}
