using Domain.Common;

namespace Infrastructure.DAL.EntityFramework
{
    public interface IEventDispatcher
    {
        void Dispatch(IEvent @event);
    }
}