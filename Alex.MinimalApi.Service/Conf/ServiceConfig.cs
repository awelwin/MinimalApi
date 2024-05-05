using Alex.MinimalApi.Service.Application.EndpointHandlers;
using Alex.MinimalApi.Service.Application.Services;
using Alex.MinimalApi.Service.Application.Validators;
using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Core.Services;
using Alex.MinimalApi.Service.Infrastructure;
using Alex.MinimalApi.Service.Infrastructure.Repository;
using Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework;
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

            //Repositories
            builder.Services.AddTransient(typeof(IRepository<Core.Notification>), typeof(GenericRepository<Core.Notification, Infrastructure.Repository.EntityFramework.Notification>));
            builder.Services.AddTransient(typeof(IRepository<Core.Employee>), typeof(GenericRepository<Core.Employee, Infrastructure.Repository.EntityFramework.Employee>));
            builder.Services.AddDbContext<MinimalApiDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("MinimalApiDb")), ServiceLifetime.Scoped);

            //Application services
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<EmployeeRouteService>();
            builder.Services.AddScoped<GenericEntityService<Core.Employee>>();
            builder.Services.AddScoped<GenericRouteService<Pres.Employee, Core.Employee>>();

            //validation
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(EmployeeValidator)); //register them all at once

            //Document
            builder.Services.AddTransient<IPublicDocumentService, AzureBlobPublicDocumentService>
                (x => new AzureBlobPublicDocumentService(
                    builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("account-name")!,
                     builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("account-key")!,
                    builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("container")!,
                    builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("host")!)); //key

        }
    }
}
