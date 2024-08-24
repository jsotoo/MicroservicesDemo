using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

namespace Microservices.Validator.Service.Infrastructure.Http.Extensions
{
    public static class HttpExtensions
    {
        public static void AddHttp(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();            
        }
    }
}
