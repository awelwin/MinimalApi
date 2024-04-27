using Alex.MinimalApi.Service.Core;
using AutoMapper;
using Microsoft.OpenApi.Models;
using core = Alex.MinimalApi.Service.Core;
using pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Presentation
{

    /// <summary>
    /// Route handler for 'Notification'  route
    /// </summary>
    internal class NotificationRouteHandler : IRouteHandler
    {
        public static void CreateRoutes(WebApplication app)
        {
            app.MapGet("/Notification/{id}",
                (int id, IRepository<core.Notification> repo, IMapper mapper) => GetNotificationById(id, repo, mapper))
                .Produces<pres.Notification>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi(operation => new(operation)
                {
                    OperationId = "get-notification-by-id",
                    Summary = "Get Notifications by id",
                    Tags = new List<OpenApiTag>() { new() { Name = "Notification" } }
                });

        }

        #region Delegates
        public static async Task<IResult> GetNotificationById(int id, IRepository<core.Notification> repo, IMapper mapper)
        {
            var result = await repo.GetAsync(id);
            if (result == null)
                return Results.NotFound(id);

            pres.Notification output = mapper.Map<pres.Notification>(result);
            return Results.Ok(output);
        }
        #endregion
    }
}
