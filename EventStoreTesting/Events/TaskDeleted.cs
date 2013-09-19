using System;

namespace EventStore.Events
{
    public class TaskDeleted : TaskEvent
    {
        public TaskDeleted(DateTime deletedAt)
        {
            DeletedAt = deletedAt;
        }

        public DateTime DeletedAt { get; set; }
    }
}
