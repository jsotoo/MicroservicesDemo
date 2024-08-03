﻿using Net.Microservices.CleanArchitecture.Core.Domain;

namespace Net.Microservices.CleanArchitecture.Core.Application
{
    /// <summary>
    /// Generic marker interface for a data model.
    /// Used to specifically identify data (persistence-related) models
    /// </summary>
    /// <typeparam name="TId">The type of Id</typeparam>
    public interface IDataEntity<TId> : IEntity<TId>
    {
    }
}
