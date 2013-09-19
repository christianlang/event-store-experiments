namespace EventStore.Events
{
    public class TaskNoteChanged : TaskEvent
    {
        public TaskNoteChanged(string note)
        {
            Note = note;
        }

        public string Note { get; private set; }

        public override string ToString()
        {
            return string.Format("Task note changed to '{0}'", Note);
        }
    }
}
