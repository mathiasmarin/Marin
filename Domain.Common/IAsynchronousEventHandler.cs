using System.Threading.Tasks;

namespace Domain.Common
{
    /// <summary>
    /// Trying this out, one interface for synchronous event and one for asynchronous events. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsynchronousEventHandler<in T> where T : IEvent
    {
        Task<bool> HandleEvent(T @event);
    }
}