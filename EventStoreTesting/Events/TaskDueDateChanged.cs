using System;

namespace EventStore.Events
{
    public class TaskDueDateChanged : TaskEvent
    {
        public TaskDueDateChanged(DateTime? dueDate)
        {
            DueDate = dueDate;
        }

        public DateTime? DueDate { get; set; }

        public override string ToString()
        {
            return string.Format("Task due date changed to '{0:d}'", DueDate);
        }
    }
}
