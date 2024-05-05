using Alex.MinimalApi.Service.Application.EndpointFilters;
using Alex.MinimalApi.Service.Application.Services;
using Microsoft.OpenApi.Models;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Application.EndpointHandlers
{
    /// <summary>
    /// Handles routes for type <p>
    /// </summary>
    /// <typeparam name="P">Presentation entity</typeparam>
    /// <typeparam name="C">CoreEntity</typeparam>
    public class RouteHandler<P, C> //: IRouteHandler
        where P : Pres.PresentationEntity
        where C : Core.CoreEntity

    {
        public static void CreateRoutes(WebApplication app, string routeBase)
        {
            //Route
            RouteService<P, C> routeService = app.Services.CreateScope().ServiceProvider.GetService<RouteService<P, C>>()!;

            app.MapPost($"/{routeBase}", (P entity) => routeService.PostAsync(entity))

                //validation
                .AddEndpointFilter<ValidationEndpointFilter<P>>()

                //Documentation
                .Produces<Pres.Employee>(StatusCodes.Status201Created)
                .WithOpenApi(op =>
                {
                    op.OperationId = $"create-{routeBase}";
                    op.Summary = $"Create {routeBase}";
                    op.Responses["201"].Description = $"Newly created {routeBase} entity with unique Id";
                    op.Tags = new List<OpenApiTag>() { new() { Name = routeBase } };
                    return op;
                });

            //Route
            app.MapGet($"/{routeBase}", () => routeService.GetAsync())

                //Documentation
                .Produces<List<P>>(StatusCodes.Status200OK)
                .WithOpenApi(op =>
                {
                    op.OperationId = $"Get-{routeBase}";
                    op.Summary = "Get Employees";
                    op.Responses["200"].Description = "Array of ${routeBase} currently in system.";
                    op.Tags = new List<OpenApiTag>() { new() { Name = $"{routeBase}" } };
                    return op;
                });
        }
    }
}
