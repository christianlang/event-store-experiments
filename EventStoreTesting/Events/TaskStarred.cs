namespace EventStore.Events
{
    public class TaskStarred : TaskEvent
    {
        public override string ToString()
        {
            return "Task starred";
        }
    }
}
