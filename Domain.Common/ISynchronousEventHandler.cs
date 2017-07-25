namespace Domain.Common
{
    /// <summary>
    /// Trying this out, one interface for synchronous event and one for asynchronous events. 
    /// If my 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISynchronousEventHandler<in T> where T : IEvent
    {
        void HandleEvent(T @event);
    }
}