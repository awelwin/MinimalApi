using Alex.MinimalApi.Service.Application.EndpointHandlers;
using Alex.MinimalApi.Service.Application.Services;
using Alex.MinimalApi.Service.Application.Validators;
using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Core.Services;
using Alex.MinimalApi.Service.Infrastructure;
using Alex.MinimalApi.Service.Infrastructure.EntityFramework;
using Alex.MinimalApi.Service.Infrastructure.Query;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Configuration
{
    /// <summary>
    /// Configure services for WebApplication
    /// </summary>
    internal static class ServiceConfig
    {

        public static void Configure(WebApplicationBuilder builder)
        {
            IMapper mapper = AutoMapperConfig.ConfigureMaps();

            //OpenAPI Documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Object mapping
            builder.Services.AddSingleton(mapper);

            //Exception handling
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            //Infrastructure Repositories
            builder.Services.AddTransient(typeof(IRepository<Core.Notification>), typeof(Repository<Core.Notification, Infrastructure.EntityFramework.Notification>));
            builder.Services.AddTransient(typeof(IRepository<Core.Employee>), typeof(Repository<Core.Employee, Infrastructure.EntityFramework.Employee>));
            builder.Services.AddDbContext<MinimalApiDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("MinimalApiDb")), ServiceLifetime.Scoped);

            //Infrastructure Query Services
            builder.Services.AddTransient<EmployeeQueryService>();

            //Application services
            builder.Services.AddScoped<EntityService<Core.Employee>>();
            builder.Services.AddScoped<RouteService<Pres.Employee, Core.Employee>>();
            builder.Services.AddScoped<EmployeeQueryRouteService>();

            //validation
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(EmployeeValidator)); //register them all at once

            //Document
            builder.Services.AddTransient<IPublicDocumentService, AzureBlobPublicDocumentService>
                (x => new AzureBlobPublicDocumentService(
                    builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("account-name")!,
                     builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("account-key")!,
                    builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("container")!,
                    builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("host")!)); //key

            //CORS FOR Development
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("localhostCorsPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200");
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });
            });

        }
    }
}
