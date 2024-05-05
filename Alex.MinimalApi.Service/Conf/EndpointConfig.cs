using Alex.MinimalApi.Service.Application.EndpointHandlers;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Configuration
{

    internal static class EndpointConfig
    {
        /// <summary>
        /// Declare and expose  HTTP endpoints / routes
        /// </summary>
        /// <param name="app">WebApplication</param>
        public static void Configure(WebApplication app)
        {
            GenericRouteHandler<Pres.Employee, Core.Employee>.CreateRoutes(app, "Employee");
            EmployeeRouteHandler.CreateRoutes(app);
            NotificationRouteHandler.CreateRoutes(app);
            PublicDocumentRouteHandler.CreateRoutes(app);
        }
    }
}
