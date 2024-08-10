using EasyNetQ;
using EasyNetQ.Topology;
using Microservices.Infrastructure.Crosscutting.Util;
using Microservices.Infrastructure.Crosscutting;
using Newtonsoft.Json;
using System;

namespace Microservices.Infrastructure.MessageBus
{
    public class TransientSubscriber : IDisposable
    {
        private SubscriptionResult subscription;

        private readonly Action<IMessage<PublishedMessage>, MessageReceivedInfo> _handler;
        private readonly string _topicFilter;
        private readonly string _messageBusEndPoint;
        private readonly string _exchangeName;

        public TransientSubscriber(string exchangeName,string messageBusEndPoint, string topicFilter, Action<IMessage<PublishedMessage>,MessageReceivedInfo> handler)
        {
            this._exchangeName = exchangeName;
            this._topicFilter = topicFilter;
            this._messageBusEndPoint = messageBusEndPoint;
            this._handler = handler;

            InitializeBus();
        }

        private void InitializeBus()
        {
            //var messageBusEndPoint = "Products.Client";
            var b =  RabbitHutch.CreateBus("host=localhost");
            var advancedBus = b.Advanced;

            var exchange = advancedBus.ExchangeDeclare(_exchangeName, ExchangeType.Topic, durable: true, autoDelete: false);
            var queue = advancedBus.QueueDeclare(_messageBusEndPoint);
            advancedBus.Bind(exchange, queue, _topicFilter);

            advancedBus.Consume<PublishedMessage>(queue, (imessage, info) => _handler(imessage, info), opt => opt.WithPrefetchCount(1));
        }

        public void Dispose()
        {
            subscription.Dispose();
        }
    }
}
