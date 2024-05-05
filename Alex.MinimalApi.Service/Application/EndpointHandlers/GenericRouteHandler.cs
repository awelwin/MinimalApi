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
    public class GenericRouteHandler<P, C> //: IRouteHandler
        where P : Pres.PresentationEntity
        where C : Core.CoreEntity

    {
        public static void CreateRoutes(WebApplication app, string routeBase)
        {
            //Route
            GenericRouteService<P, C> routeservice = app.Services.CreateScope().ServiceProvider.GetService<GenericRouteService<P, C>>()!;
            app.MapPost($"/{routeBase}", (P entity) => routeservice.PostAsync(entity))

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
        }
    }
}
