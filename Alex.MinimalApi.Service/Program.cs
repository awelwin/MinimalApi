using Alex.MinimalApi.Service;
using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Presentation;
using Alex.MinimalApi.Service.Repository;
using Alex.MinimalApi.Service.Repository.EntityFramework;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using core = Alex.MinimalApi.Service.Core;
using EF = Alex.MinimalApi.Service.Repository.EntityFramework;


var builder = WebApplication.CreateBuilder(args);
IMapper mapper = AutoMapperConfig.ConfigureMaps();

#region services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MinimalApiDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("MinimalApiDb")), ServiceLifetime.Scoped);
builder.Services.AddSingleton<IMapper>(mapper);
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient(
    typeof(IRepository<core.Notification>),
    typeof(GenericRepository<core.Notification, EF.Notification>)
    );

var app = builder.Build();
#endregion services

#region RequestPipeline
app.UseHttpsRedirection();
app.UsePathBase("/api/v1");
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

#region Endpoint-Routes

NotificationRouteHandler.CreateRoutes(app);
EmployeeRouteHandler.CreateRoutes(app);

#endregion

app.Run();


