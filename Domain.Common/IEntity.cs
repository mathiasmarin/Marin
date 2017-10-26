using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
        Guid Id { get; }
    }
}