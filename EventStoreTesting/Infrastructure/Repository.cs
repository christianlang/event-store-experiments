using System;
using System.Collections.Generic;
using System.Linq;

namespace EventStore.Infrastructure
{
    public class Repository<T> : IEventSourcedRepository<T>
         where T : class, IEventSourced
    {
        private readonly Store _store;
        private readonly Func<string, IEnumerable<IVersionedEvent>, T> _entityFactory;

        public Repository(Store store)
        {
            _store = store;

            var constructor = typeof(T).GetConstructor(new[] { typeof(string), typeof(IEnumerable<IVersionedEvent>) });
            if (constructor == null)
            {
                throw new InvalidCastException("Type T must have a constructor with the following signature: .ctor(String, IEnumerable<IVersionedEvent>)");
            }
            _entityFactory = (id, events) => (T)constructor.Invoke(new object[] { id, events });
        }

        public T Find(string id)
        {
            var events = _store.Events
                               .Where(e => e.SourceId == id)
                               .OrderBy(x => x.Version);

            if (events.Any())
            {
                return _entityFactory.Invoke(id, events);
            }

            return null;
        }

        public T Get(string id)
        {
            var entity = Find(id);
            if (entity == null)
            {
                throw new ArgumentException(id);
            }

            return entity;
        }

        public void Save(T eventSourced)
        {
            var events = eventSourced.Events.ToArray();
            _store.AddEvents(events);

            EventBus.Current.Publish(events);
        }
    }
}
