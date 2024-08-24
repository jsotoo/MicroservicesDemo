
using Microservices.Saga.Orchestrator.Service.Application.Extensions;
using Microservices.Saga.Orchestrator.Service.Infrastructure.Extensions;
using Microservices.Saga.Orchestrator.Service.Infrastructure.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.RegisterEndpoints();

app.Run();