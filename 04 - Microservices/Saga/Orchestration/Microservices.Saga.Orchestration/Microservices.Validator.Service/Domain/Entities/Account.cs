using Microservices.Messages.Base;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Messages.Enums;

namespace Microservices.Validator.Service.Domain.Entities
{
    public class Account: IEntity
    {
        private Guid _accountId;

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id
        {
            get => _accountId;
            set => _accountId = value;
        }

        [BsonElement("accountId")]
        public Guid AccountId
        {
            get => _accountId;
            set => _accountId = value;
        }

        [BsonElement("ownerName")]
        public string OwnerName { get; private set; }

        [BsonElement("balance")]
        public decimal Balance { get; private set; }

        [BsonElement("isActive")]
        public bool IsActive { get; private set; }

        public Account(Guid accountId, string ownerName, decimal balance, bool isActive)
        {
            AccountId = accountId;
            OwnerName = ownerName;
            Balance = balance;
            IsActive = isActive;
        }
        public bool Validate(decimal amount, TransferType type)
        {
            if (type == TransferType.Debit)
            {
                return IsActive && (Balance - amount) >= 0;
            }
            else
            {
                return IsActive;
            }
        }
    }
}
