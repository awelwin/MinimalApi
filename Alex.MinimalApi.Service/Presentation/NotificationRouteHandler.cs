using Alex.MinimalApi.Service.Core;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Pres = Alex.MinimalApi.Service.Presentation;

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
                (int id, IRepository<Core.Notification> repo, IMapper mapper) => GetNotificationById(id, repo, mapper))
                .Produces<Pres.Notification>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi(operation => new(operation)
                {
                    OperationId = "get-notification-by-id",
                    Summary = "Get Notifications by id",
                    Tags = new List<OpenApiTag>() { new() { Name = "Notification" } }
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
