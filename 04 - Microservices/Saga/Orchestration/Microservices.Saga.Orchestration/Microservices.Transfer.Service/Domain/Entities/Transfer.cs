using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Messages.Enums;

namespace Microservices.Transfer.Service.Domain.Entities
{
    public class Transfer: IEntity
    {
        private Guid _transferId;

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id
        {
            get => _transferId;
            set => _transferId = value;
        }

        [BsonElement("transferId")]
        public Guid TransferId
        {
            get => _transferId;
            set => _transferId = value;
        }
        [BsonElement("accountId")]
        public Guid AccountId { get; set; }

        [BsonElement("transferDate")]
        public DateTime TransferDate { get; private set; }

        [BsonElement("amount")]
        public decimal Amount { get; private set; }

        [BsonElement("type")]
        public TransferType Type { get; private set; }

        public Transfer(Guid transferId,Guid accountId, DateTime transferDate, decimal amount, TransferType type)
        {
            TransferId = transferId;
            AccountId = accountId;
            Type = type;
            TransferDate = transferDate;
            Amount = amount;
        }        
    }
}
