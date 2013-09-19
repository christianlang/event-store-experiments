using System;
using System.Collections.Generic;

namespace EventStore.Infrastructure
{
    public class EventSourced : IEventSourced
    {
        private readonly Dictionary<Type, Action<IVersionedEvent>> _handlers = new Dictionary<Type, Action<IVersionedEvent>>();
        private readonly List<IVersionedEvent> _pendingEvents = new List<IVersionedEvent>();
        
        private readonly string _id;
        private int _version = -1;

        protected EventSourced(string id)
        {
            _id = id;
        }

        public string Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets the entity's version. As the entity is being updated and events being generated, the version is incremented.
        /// </summary>
        public int Version
        {
            get { return _version; }
            protected set { _version = value; }
        }

        /// <summary>
        /// Gets the collection of new events since the entity was loaded, as a consequence of command handling.
        /// </summary>
        public IEnumerable<IVersionedEvent> Events
        {
            get { return _pendingEvents; }
        }

        /// <summary>
        /// Configures a handler for an event. 
        /// </summary>
        protected void Handles<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent
        {
            _handlers.Add(typeof (TEvent), e => handler((TEvent) e));
        }

        protected void LoadFrom(IEnumerable<IVersionedEvent> pastEvents)
        {
            foreach (var e in pastEvents)
            {
                _handlers[e.GetType()].Invoke(e);
                _version = e.Version;
            }
        }

        protected void Update(VersionedEvent e)
        {
            e.SourceId = _id;
            e.Version = _version + 1;
            _handlers[e.GetType()].Invoke(e);
            _version = e.Version;
            _pendingEvents.Add(e);
        }
    }
}
