using System;
using System.Collections.Generic;

namespace EventStore.Infrastructure
{
    public interface IEventBus : IObservable<IEvent>
    {
        void Publish(IEvent e);

        void Publish(IEnumerable<IEvent> events);
    }
}
