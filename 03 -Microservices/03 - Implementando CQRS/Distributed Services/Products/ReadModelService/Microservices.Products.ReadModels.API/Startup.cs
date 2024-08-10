using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using Microservices.Infrastructure.Crosscutting;
using Microservices.Infrastructure.Crosscutting.Util;
using Microservices.Infrastructure.MessageBus;
using Microservices.Infrastructure.Repository;
using Microservices.Products.Infrastructure.Dto;
using Microservices.Products.ReadModels.API.Views;
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
using StackExchange.Redis;
using Aggregate = Microservices.Infrastructure.Crosscutting.Aggregate;

namespace Microservices.Products.ReadModels.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Products.ReadModels.API", Version = "v1" });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Products.ReadModels.API V1");
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
            var redis = ConnectionMultiplexer.Connect("localhost");
            var productView = new ProductView(new RedisReadModelRepository<ProductDto>(redis.GetDatabase(), new RabbitMqBus(RabbitHutch.CreateBus("host=localhost"))));
            ServiceLocator.ProductView = productView;

            var eventMappings = new EventHandlerDiscovery()
                                .Scan(productView)
                                .Handlers;

            var messageBusEndPoint = "Products.Read";
            var topicFilter = "Products.Events";
            var exchangeName = "Products";

            var b = RabbitHutch.CreateBus("host=localhost");
            var advancedBus = b.Advanced;
            var exchange = advancedBus.ExchangeDeclare(exchangeName, ExchangeType.Topic, durable: true, autoDelete: false);
            var queue = advancedBus.QueueDeclare(messageBusEndPoint);
            advancedBus.Bind(exchange, queue, topicFilter);

            advancedBus.Consume<PublishedMessage>(queue, (imessage, info) =>
            {
                var message = imessage.Body;
                var messageType = Type.GetType(message.MessageTypeName);
                if (messageType != null)
                {                    
                    EventHandlerData eventHandlerData;
                    var handlerFound = eventMappings.TryGetValue(messageType.Name, out eventHandlerData);
                    if (handlerFound)
                    {
                        var @event = JsonConvert.DeserializeObject(message.SerializedMessage, eventHandlerData.TypeParameter);
                        //Console.WriteLine("Start Process - " + messageType.Name);
                        eventHandlerData.AggregateHandler.AsDynamic().ApplyEvent(@event, ((Event)@event).Version);
                        //Console.WriteLine("End Process - " + messageType.Name);
                    }
                }
            }, opt =>
            {
                opt.WithPrefetchCount(1);
            });
            
            var bus = new RabbitMqBus(b);
            ServiceLocator.Bus = bus;
        }
    }
}
