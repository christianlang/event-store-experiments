namespace EventStore.Events
{
    public class TaskUnstarred : TaskEvent
    {
        public override string ToString()
        {
            return "Task unstarred";
        }
    }
}
