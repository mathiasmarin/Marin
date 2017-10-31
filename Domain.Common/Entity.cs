using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class Entity : IEntity
    {
        private readonly IDictionary<Type, IEvent> _events = new ConcurrentDictionary<Type, IEvent>();
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; protected set; }
        public DateTime Created { get; } = DateTime.Now;
        public IEnumerable<IEvent> Events => _events.Values;
        /// <summary>
        /// Add event to this entity. If event of this type already exist it will be overwritten. 
        /// Be advised that the key is the type of event and type is not always failsafe when it comes to inherritance
        /// If an eventhandler exist for this event, it will be called after SaveChanges is completed. 
        /// </summary>
        /// <param name="event"></param>
        protected void AddEvent(IEvent @event)
        {
            _events[@event.GetType()] = @event;
        }
    }
}