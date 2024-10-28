using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTracing;
using OpenTracing.Util;
using System.Reflection;
using Tracer = Jaeger.Tracer;

namespace Microservices.Demo.Pricing.API.Infrastructure.Jaeger
{
    public static class JaegerServiceCollectionExtensions
    {        
        private static readonly Uri _jaegerUri = new Uri("http://microservices.demo.jaeger:4317");
        private static readonly Uri _otelUri = new Uri("http://microservices.demo.otel-collector:4317");

        public static IServiceCollection AddJaeger(this IServiceCollection services)
        {
            string serviceName = Assembly.GetEntryAssembly().GetName().Name;

            services.AddOpenTelemetry().WithTracing(builder =>
            {
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation(opts => opts.SetDbStatementForText = true)                
                .AddOtlpExporter(opts => { opts.Endpoint = _jaegerUri; });
            });

            return services;
        }
        public static IServiceCollection AddOtel(this IServiceCollection services)
        {
            string serviceName = Assembly.GetEntryAssembly().GetName().Name;

            services.AddOpenTelemetry().WithTracing(builder =>
            {
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation(opts => opts.SetDbStatementForText = true)
                .AddOtlpExporter(opts => {
                    opts.Endpoint = _otelUri;
                    opts.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            });

            services.AddOpenTelemetry().WithMetrics(builder =>
            {
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter(serviceName)
                .AddOtlpExporter(opts => {
                    opts.Endpoint = _otelUri;
                    opts.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            });

            return services;
        }
    }
}
