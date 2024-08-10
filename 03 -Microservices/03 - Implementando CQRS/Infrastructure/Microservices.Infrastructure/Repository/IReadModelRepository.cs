using Microservices.Infrastructure.Crosscutting;
using System;
using System.Collections.Generic;

namespace Microservices.Infrastructure.Repository
{
    public interface IReadModelRepository<T>
        where T : ReadObject
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Update(T t,Event e = null);
        void Insert(T t, Event e = null);
    }
}