﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Core.Application.Exceptions;
using Net.Microservices.CleanArchitecture.Core.Domain;
using Net.Microservices.CleanArchitecture.Infrastructure.DbContexts;
using Net.Microservices.CleanArchitecture.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Net.Microservices.CleanArchitecture.Infrastructure
{
    /// <summary>
    /// An implementation of <see cref="IEventStore"/> using Entity Framework
    /// </summary>
    /// <inheritdoc cref="IEventStore"/>
    internal class EFEventStore : IEventStore
    {
        #region Private Fields

        private readonly EventStoreDbContext _context;
        private readonly DbSet<EventEntity> _events;

        #endregion Private Fields

        #region Public Constructors

        public EFEventStore(EventStoreDbContext context) {
            _context = context;
            _events = _context.Set<EventEntity>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc cref="IEventStore.LoadAsync{TAggregateId}(string, string, int, int)"/>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IReadOnlyCollection<IDomainEvent<TAggregateId>>> LoadAsync<TAggregateId>(string aggregateRootId, string aggregateName, int fromVersion, int toVersion) {
            Guard.Against.Negative(fromVersion, nameof(fromVersion));
            Guard.Against.Negative(toVersion, nameof(toVersion));
            if (fromVersion > toVersion) {
                throw new ArgumentException($"{nameof(fromVersion)} cannot be grated than {nameof(toVersion)}");
            }
            IQueryable<EventEntity> events = _events.Where(e => e.AggregateId == aggregateRootId && e.AggregateName == aggregateName && e.Version >= fromVersion && e.Version <= toVersion).OrderBy(de => de.Version);
            var domainEvents = new List<IDomainEvent<TAggregateId>>();
            //get events
            foreach (var @event in events) {
                var domainEvent = DomainEventHelper.ConstructDomainEvent<TAggregateId>(@event.Data, @event.AssemblyTypeName);
                domainEvents.Add(domainEvent);
            }

            return domainEvents.AsReadOnly();
        }

        public async Task SaveAsync<TAggregateId>(string aggregateName, int expectedVersion, IEnumerable<IDomainEvent<TAggregateId>> domainEvents) {
            foreach (var domainEvent in domainEvents) {
                EventEntity eventEntity = ConstructEventEntity(domainEvent, expectedVersion, aggregateName);
                await _events.AddAsync(eventEntity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync<TAggregateId>(string aggregateName, int expectedVersion, IDomainEvent<TAggregateId> domainEvent) {

            EventEntity eventEntity = ConstructEventEntity(domainEvent, expectedVersion, aggregateName);
            await _events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        #endregion Public Methods

        private EventEntity ConstructEventEntity<TAggregateId>(IDomainEvent<TAggregateId> domainEvent, int expectedVersion, string aggregateName) {
            if (domainEvent.AggregateVersion > expectedVersion)
                throw new EventStoreException($"Concurrency issue detected when saving events. Event found with version {domainEvent.AggregateVersion} which is larger than maximum expected version {expectedVersion}");
            Type domainEventType = domainEvent.GetType();
            return new EventEntity()
            {
                Id = domainEvent.EventId,
                AggregateId = domainEvent.AggregateId.ToString(),
                AggregateName = aggregateName,
                Name = domainEventType.Name,
                AssemblyTypeName = domainEventType.AssemblyQualifiedName,
                Data = JsonConvert.SerializeObject(domainEvent),
                Version = domainEvent.AggregateVersion,
                CreatedAt = DateTime.UtcNow //duplicate. save or lose??
            };
        }
    }
}