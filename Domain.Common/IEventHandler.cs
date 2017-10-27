namespace Domain.Common
{
    public interface IEventHandler<in T> where T:IEvent
    {
        
    }
}