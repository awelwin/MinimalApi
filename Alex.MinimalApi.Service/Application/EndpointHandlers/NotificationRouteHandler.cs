using Alex.MinimalApi.Service.Core.Services;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Application.EndpointHandlers
{

    /// <summary>
    /// Route handler for 'Notification'  route
    /// </summary>
    public sealed class NotificationRouteHandler : IRouteHandler
    {
        public static void CreateRoutes(WebApplication app)
        {
            //Route
            app.MapGet("/Notification/{id}",
                (int id, IRepository<Core.Notification> repo, IMapper mapper) => GetNotificationById(id, repo, mapper))

                //Documentation
                .Produces<Pres.Notification>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi(op =>
                {
                    op.OperationId = "get-notification-by-id";
                    op.Summary = "Get Notifications by id";
                    op.Parameters[0].Description = "Unique notification Id";
                    op.Responses["200"].Description = "Notoficatin matching Id parameter";
                    op.Tags = new List<OpenApiTag>() { new() { Name = "Notification" } };
                    return op;
                });

        }

        #region Delegates
        public static async Task<IResult> GetNotificationById(int id, IRepository<Core.Notification> repo, IMapper mapper)
        {

            var result = await repo.GetAsync(id);
            if (result == null)
                return Results.NotFound(id);

            Pres.Notification output = mapper.Map<Pres.Notification>(result);
            return Results.Ok(output);
        }
        #endregion
    }
}
