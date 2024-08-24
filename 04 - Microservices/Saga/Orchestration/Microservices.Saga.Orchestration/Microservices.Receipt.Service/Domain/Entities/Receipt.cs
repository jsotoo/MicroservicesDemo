using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Messages.Enums;

namespace Microservices.Receipt.Service.Domain.Entities
{
    public class Receipt: IEntity
    {
        private Guid _receiptId;

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id
        {
            get => _receiptId;
            set => _receiptId = value;
        }

        [BsonElement("receiptId")]
        public Guid ReceiptId
        {
            get => _receiptId;
            set => _receiptId = value;
        }
        [BsonElement("accountId")]
        public Guid AccountId { get; set; }
                
        [BsonElement("amount")]
        public decimal Amount { get; private set; }

        [BsonElement("type")]
        public TransferType Type { get; private set; }

        public Receipt(Guid receiptId, Guid accountId, DateTime transferDate, decimal amount, TransferType type)
        {
            ReceiptId = receiptId;
            AccountId = accountId;
            Type = type;            
            Amount = amount;
        }        
    }
}
