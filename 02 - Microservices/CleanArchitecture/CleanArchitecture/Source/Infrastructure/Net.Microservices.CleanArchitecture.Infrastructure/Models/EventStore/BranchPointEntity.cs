using System;
using System.Collections.Generic;
using Net.Microservices.CleanArchitecture.Core.Application;

namespace Net.Microservices.CleanArchitecture.Infrastructure.Models
{
    public class BranchPointEntity : DataEntityBase<int>
    {
        /// <summary>
        /// Branch point indicative name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// FK for the Event Entity
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// The type of the branch point
        /// </summary>
        public BranchPointTypeEnum Type { get; set; }

        /// <summary>
        /// Navigation property of the event
        /// </summary>
        public virtual EventEntity Event { get; set; }

        public virtual ICollection<RetroactiveEventEntity> RetroactiveEvents { get; set; }
    }
}