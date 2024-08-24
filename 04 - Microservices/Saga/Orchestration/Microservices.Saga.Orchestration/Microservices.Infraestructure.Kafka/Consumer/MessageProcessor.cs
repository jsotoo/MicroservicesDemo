using MediatR;
using Microservices.Messages.Base;

namespace Microservices.Infrastructure.Kafka.Consumer
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IMediator _mediator;
        public MessageProcessor(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ProcessCommand(Message message)
        {
            await _mediator.Send((dynamic)message);
        }

        public async Task ProcessEvent(Message message)
        {
            await _mediator.Send((dynamic)message);
        }

    }
}
