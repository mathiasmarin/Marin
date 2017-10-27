using Domain.Common;
using SimpleInjector;

namespace Infrastructure.DAL.EntityFramework
{
    public class SimpleInjectorEventDispatcher : IEventDispatcher
    {
        private readonly Container _container;
        public SimpleInjectorEventDispatcher(Container container)
        {
            _container = container;
        }

        public void Dispatch(IEvent theEvent)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(theEvent.GetType());
            var handlers = _container.GetAllInstances(handlerType);

            foreach (dynamic handler in handlers)
            {
                handler.Handle((dynamic)theEvent);
            }
        }
    }
}