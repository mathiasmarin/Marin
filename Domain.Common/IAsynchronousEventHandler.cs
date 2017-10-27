using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IAsynchronousEventHandler<in T>: IEventHandler<T> where T : IEvent
    {
        /// <summary>
        ///     Handles the event.
        /// </summary>
        /// <param name="event">Event object.</param>
        Task Handle(T @event);
    }
}