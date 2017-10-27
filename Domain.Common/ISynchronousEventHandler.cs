namespace Domain.Common
{
    public interface ISynchronousEventHandler<in T> :IEventHandler<T> where T : IEvent
    {
        /// <summary>
        ///     Handles the event.
        /// </summary>
        /// <param name="event">Event object.</param>
        void Handle(T @event);
    }
}