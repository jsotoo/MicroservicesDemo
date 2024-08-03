﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Crosscutting.Exceptions
{
    public class ReadModelNotFoundException : Exception
    {
        public readonly Guid Id;
        public readonly Type Type;

        public ReadModelNotFoundException(Guid id, Type type)
            : base(string.Format("ReadModel '{0}' (type {1}) was not found.", id, type.Name))
        {
            Id = id;
            Type = type;
        }
    }
}
