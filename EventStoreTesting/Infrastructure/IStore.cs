using System.Collections.Generic;

namespace EventStore.Infrastructure
{
    public interface IStore
    {
        IEnumerable<IVersionedEvent> Events { get; }

        void AddEvents(IEnumerable<IVersionedEvent> events);
    }
}