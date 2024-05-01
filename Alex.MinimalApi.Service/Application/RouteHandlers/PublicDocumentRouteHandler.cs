using Alex.MinimalApi.Service.Core;
using Microsoft.AspNetCore.Mvc;

namespace Alex.MinimalApi.Service.Application.RouteHandlers
{
    /// <summary>
    /// Route handler for '/Employee'  route
    /// </summary>
    public class PublicDocumentRouteHandler : IRouteHandler
    {
        public static void CreateRoutes(WebApplication app)
        {
            //Route
            app.MapPost("/PublicDocument",
                ([FromForm] IFormFile file, IPublicDocumentService service) => CreateDocument(file, service))

            //Documentation
            .Produces(StatusCodes.Status201Created)
            .DisableAntiforgery();
        }

        #region Delegates

        public static async Task<IResult> CreateDocument(IFormFile file, IPublicDocumentService documentService)
        {
            //validate
            if (file == null)
                return Results.BadRequest("file not specified");

            //persist
            string id = await documentService.CreateAsync(file.OpenReadStream());

            //acknowledge
            return Results.Created($"/PublicDocument/{id}", null!);
        }

        #endregion
    }
}
