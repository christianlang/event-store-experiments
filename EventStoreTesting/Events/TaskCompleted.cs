using System;

namespace EventStore.Events
{
    public class TaskCompleted : TaskEvent
    {
        public TaskCompleted(DateTime completedAt)
        {
            CompletedAt = completedAt;
        }

        public DateTime CompletedAt { get; private set; }

        public override string ToString()
        {
            return string.Format("Task completed at '{0}'", CompletedAt);
        }
    }
}
