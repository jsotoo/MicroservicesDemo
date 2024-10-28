using MediatR;
using Microservices.Demo.Report.API.Application;
using Microsoft.Extensions.Logging.Debug;
using Serilog;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration.ConfigServer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.ConfigureLogging(loggign =>
{
    loggign.SetMinimumLevel(LogLevel.Information);
    loggign.AddConsole();
})
.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
{
    ILoggerFactory factory = new LoggerFactory();
    var provider = new DebugLoggerProvider();
    factory.AddProvider(provider);

    var hostingEnvironment = webHostBuilderContext.HostingEnvironment;
    configurationBuilder.AddConfigServer(hostingEnvironment.EnvironmentName, factory);
});

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
               .WriteTo.Http("http://microservices.demo.logstash:28080", null)
               .CreateLogger();

builder.Services.AddOtel();
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddDataServices(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
