using Alex.MinimalApi.Service.Application.Services;
using Alex.MinimalApi.Service.Infrastructure.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Alex.MinimalApi.Service.Application.EndpointHandlers
{
    public class QueryRouteHandler
    {
        public static void CreateRoutes(WebApplication app)
        {

            EmployeeQueryRouteService employeeQueryRouteService = app.Services.CreateScope().ServiceProvider.GetService<EmployeeQueryRouteService>()!;

            #region Employee
            //Route
            app.MapGet($"/query/employee-search", ([FromQuery] string input) => employeeQueryRouteService.EmployeeSearch(input))

                //Documentation
                .Produces<List<EmployeeSearchQueryResult>>(StatusCodes.Status200OK)
                .WithOpenApi(op =>
                {
                    op.OperationId = $"query-EmployeeSearch";
                    op.Summary = $"EmployeeSearch";
                    op.Responses["200"].Description = "Array of EmployeeSearchQueryResult";
                    op.Tags = new List<OpenApiTag>() { new() { Name = $"query" } };
                    return op;
                });

            #endregion
        }
    }
}