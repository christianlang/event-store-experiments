namespace EventStore.Events
{
    public class TaskTitleChanged : TaskEvent
    {
        public TaskTitleChanged(string title)
        {
            Title = title;
        }

        public string Title { get; private set; }

        public override string ToString()
        {
            return string.Format("Task title changed to '{0}'", Title);
        }
    }
}
