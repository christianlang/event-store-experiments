namespace EventStore.Events
{
    public class TaskIncompleted : TaskEvent
    {
        public override string ToString()
        {
            return "Task incompleted";
        }
    }
}
