using Alex.MinimalApi.Service;
using Alex.MinimalApi.Service.Application.RouteHandlers;
using Alex.MinimalApi.Service.Application.Validators;
using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Infrastructure;
using Alex.MinimalApi.Service.Infrastructure.Repository;
using Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Core = Alex.MinimalApi.Service.Core;


var builder = WebApplication.CreateBuilder(args);
IMapper mapper = AutoMapperConfig.ConfigureMaps();

#region services

//OpenAPI Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Object mapping
builder.Services.AddSingleton<IMapper>(mapper);

//Exception handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Repositories
builder.Services.AddTransient(typeof(IRepository<Core.Notification>), typeof(GenericRepository<Core.Notification, Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework.Notification>));
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddDbContext<MinimalApiDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("MinimalApiDb")), ServiceLifetime.Scoped);

//validation
builder.Services.AddValidatorsFromAssemblyContaining(typeof(EmployeeValidator)); //register them all at once

//Document
builder.Services.AddTransient<IPublicDocumentService, AzureBlobPublicDocumentService>
    (x => new AzureBlobPublicDocumentService(
        builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("account-name")!,
         builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("account-key")!,
        builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("container")!,
        builder.Configuration.GetSection("AzureBlobPublicDocumentService").GetValue<string>("host")!)); //key

var app = builder.Build();
#endregion services

#region RequestPipeline
app.UseHttpsRedirection();
app.UsePathBase("/api/v1");
app.UseRouting();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
#endregion

#region Endpoint-Routes

NotificationRouteHandler.CreateRoutes(app);
EmployeeRouteHandler.CreateRoutes(app);
PublicDocumentRouteHandler.CreateRoutes(app);

#endregion

app.Run();


