namespace Alex.MinimalApi.Service.Configuration
{

    internal static class MiddlewareConfig
    {
        /// <summary>
        /// Configure the HTTP Request Pipeline middleware
        /// </summary>
        /// <param name="app">WebApplication</param>
        public static void Configure(WebApplication app)
        {
            if (!app.Environment.IsEnvironment("developmentDocker"))
                app.UseHttpsRedirection();

            app.UsePathBase("/api/v1");
            app.UseRouting();
            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("developmentDocker"))
            {
                app.UseCors("localhostCorsPolicy");
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();


            }
        }
    }
}
