using EventStore.Infrastructure;

namespace EventStore.Events
{
    public class TaskCreated : VersionedEvent
    {
        public TaskCreated(string listId)
        {
            ListId = listId;
        }

        public string ListId { get; private set; }

        public override string ToString()
        {
            return string.Format("Task '{0}' created in list '{1}'", SourceId, ListId);
        }
    }
}
