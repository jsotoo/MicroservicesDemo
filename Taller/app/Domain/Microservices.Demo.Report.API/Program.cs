using Elastic.Apm.NetCoreAll;
using MediatR;
using Microservices.Demo.Report.API.Application;
using Microservices.Demo.Report.API.Infrastructure.Configuration;
using Microservices.Demo.Report.API.Infrastructure.Jaeger;
using Microsoft.Extensions.Logging.Debug;
using Newtonsoft.Json.Serialization;
using Serilog;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration.ConfigServer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(loggign =>
{
    loggign.SetMinimumLevel(LogLevel.Information);
    loggign.AddConsole();
});

builder.Host.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
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

builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAllElasticApm(builder.Configuration);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
