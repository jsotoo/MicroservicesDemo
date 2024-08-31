using Steeltoe.Management.Endpoint;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAllActuators(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello from net-prometheus!");

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapAllActuators(null); });

app.Run();
