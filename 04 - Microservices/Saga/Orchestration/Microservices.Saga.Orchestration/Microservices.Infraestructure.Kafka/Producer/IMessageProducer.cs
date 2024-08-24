using Microservices.Messages.Base;

namespace Microservices.Infrastructure.Kafka.Producer
{
    public interface IMessageProducer
    {
        void Dispose();
        Task SendMessageAsync(Message message);
    }
}