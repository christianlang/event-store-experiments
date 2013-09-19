using System.Collections.Generic;
using EventStore.Events;
using EventStore.Infrastructure;

namespace EventStore
{
    public class TaskList : EventSourced
    {
        protected TaskList(string id)
            : base(id)
        {
            Handles<ListCreated>(e => {});
            Handles<ListTitleChanged>(e => Title = e.Title);
        }

        /// <summary>
        /// Loads an existing list, specifying the past events.
        /// </summary>
        /// <param name="id">The local ID.</param>
        /// <param name="history">The event stream of this event sourced object.</param>
        public TaskList(string id, IEnumerable<IVersionedEvent> history)
            : this(id)
        {
            LoadFrom(history);
        }

        /// <summary>
        /// Creates a new list.
        /// </summary>
        /// <param name="id">The local ID.</param>
        /// <param name="title">The list title.</param>
        public TaskList(string id, string title) 
            : this(id)
        {
            Update(new ListCreated());
            Update(new ListTitleChanged(title));
        }

        public string Title { get; private set; }

        public TaskList ChangeTitle(string value)
        {
            Update(new ListTitleChanged(value));
            return this;
        }
    }
}
