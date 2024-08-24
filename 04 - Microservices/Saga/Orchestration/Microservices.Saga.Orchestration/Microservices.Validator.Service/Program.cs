
using Microservices.Validator.Service.Application.Extensions;
using Microservices.Validator.Service.Infrastructure.Extensions;
using Microservices.Validator.Service.Infrastructure.Http.Extensions;
using Microservices.Validator.Service.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.RegisterEndpoints();
app.UseSeedData();
app.Run();