namespace Microservices.Demo.Report.API.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ReportApplicationService>();

            return services;
        }
    }
}
