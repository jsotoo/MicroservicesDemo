using Net.Microservices.CleanArchitecture.Core.Application;

namespace Net.Microservices.CleanArchitecture.Infrastructure.Models
{
    /// <summary>
    /// Base data entity class to be inherited from every entity in database context
    /// Inherits <see cref="AuditableEntity"/> for convenience.
    /// </summary>
    public class DataEntityBase<TId> : AuditableEntity, IDataEntity<TId>
    {
        public TId Id { get; set; }
    }
}