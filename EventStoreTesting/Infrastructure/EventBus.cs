using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace EventStore.Infrastructure
{
    public class EventBus : IEventBus
    {
        private static IEventBus _current = new EventBus();

        public static IEventBus Current
        {
            get { return _current; }
            set { _current = value; }
        }

        private readonly Subject<IEvent> _events = new Subject<IEvent>();

        public void Publish(IEvent e)
        {
            _events.OnNext(e);
        }

        public void Publish(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                Publish(e);
            }
        }

        public IDisposable Subscribe(IObserver<IEvent> observer)
        {
            return _events.Subscribe(observer);
        }
    }
}
