﻿namespace Alex.MinimalApi.Service.Presentation
{
    /// <summary>
    /// Route Handler operations
    /// </summary>

    public interface IRouteHandler
    {
        /// <summary>
        /// Add Endpoint routes to webapplication
        /// </summary>
        /// <param name="app">web application</param>
        static abstract void CreateRoutes(WebApplication app);
    }
}
