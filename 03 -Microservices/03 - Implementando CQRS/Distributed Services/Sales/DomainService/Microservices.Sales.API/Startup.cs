using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using EventStore.ClientAPI;
using Microservices.Infrastructure.Crosscutting;
using Microservices.Infrastructure.Crosscutting.Util;
using Microservices.Infrastructure.MessageBus;
using Microservices.Infrastructure.Repository;
using Microservices.Sales.API.MicroServices.Orders.Handlers;
using Microservices.Sales.API.MicroServices.Products.View;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Microservices.Sales.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigureHandlers();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Sales.API", Version = "v1" });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Sales.API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureHandlers()
        {   
            var bus = new RabbitMqBus(RabbitHutch.CreateBus("host=localhost"));
            ServiceLocator.Bus = bus;

            var eventStoreConnection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            eventStoreConnection.ConnectAsync().Wait();
            var repository = new EventStoreRepository(eventStoreConnection, bus);

            ServiceLocator.OrderCommands = new OrderCommandHandlers(repository);
            ServiceLocator.ProductView = new ProductView();              
        }
    }
}
