using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using Microservices.Infrastructure.Crosscutting;
using Newtonsoft.Json;
using IMessage = Microservices.Infrastructure.Crosscutting.IMessage;

namespace Microservices.Infrastructure.MessageBus
{
    public class RabbitMqBus : IMessageBus
    {
        private readonly IBus bus;

        public IBus Bus { get { return bus; } }

        public RabbitMqBus(IBus easyNetQBus)
        {
            if (easyNetQBus == null)
            {
                throw new ArgumentNullException("easyNetQBus");
            }
            bus = easyNetQBus;
        }

        public void Publish<T>(T @event) where T : Event
        {
            if (bus != null)
            {                
                var innerMessage = JsonConvert.SerializeObject(@event);
                var eventType = @event.GetType();

                var advancedBus = bus.Advanced;
                var exchangeName = GetExchangeForEvent(eventType);
                var exchange = advancedBus.ExchangeDeclare(exchangeName, ExchangeType.Topic, durable: true);
                                
                var topicName = GetTopicForEvent(eventType);

                var message = new Message<PublishedMessage>(new PublishedMessage()
                {
                    MessageTypeName = eventType.AssemblyQualifiedName,
                    SerializedMessage = innerMessage
                });

                var routingKey = topicName;
                advancedBus.Publish(exchange, routingKey, false,message);

                //bus.PubSub.PublishAsync(message, topicName).Wait();
            }
            else
            {
                throw new ApplicationException("RabbitMqBus is not yet Initialized");
            }
        }

        void IMessageBus.Send<T>(T command)
        {
            throw new NotImplementedException();
        }

        private string GetTopicForEvent(Type evenType)
        {            
            string eventNamespace = evenType.Namespace;

            switch (eventNamespace)
            {
                case "Microservices.Products.Infrastructure.Events":
                    return "Products.Events";
                case "Microservices.Products.Infrastructure.Notifications":
                    return "Products.Notifications";
                case "Microservices.Sales.Infrastructure.Events":
                    return "Sales.Events";

                default:
                    throw new ApplicationException($"No topic mapping found for namespace: {eventNamespace}");
            }
        }
        private string GetExchangeForEvent(Type evenType)
        {
            string eventNamespace = evenType.Namespace;

            switch (eventNamespace)
            {
                case "Microservices.Products.Infrastructure.Events":
                    return "Products";
                case "Microservices.Products.Infrastructure.Notifications":
                    return "Notifications";
                case "Microservices.Sales.Infrastructure.Events":
                    return "Sales";

                default:
                    throw new ApplicationException($"No topic mapping found for namespace: {eventNamespace}");
            }
        }
    }

    public class PublishedMessage : IMessage
    {
        public string MessageTypeName { get; set; }
        public string SerializedMessage { get; set; }
    }

    
}
