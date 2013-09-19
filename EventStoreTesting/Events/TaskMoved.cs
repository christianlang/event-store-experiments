using EventStore.Infrastructure;

namespace EventStore.Events
{
    public class TaskMoved : VersionedEvent
    {
        public TaskMoved(string fromListId, string toListId)
        {
            FromListId = fromListId;
            ToListId = toListId;
        }

        public string FromListId { get; private set; }

        public string ToListId { get; private set; }
    }
}
