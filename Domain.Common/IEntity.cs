using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public interface IEntity
    {
        Guid Id { get; }
        IEnumerable<IEvent> Events { get; }
    }
}
