namespace EventStore.Events
{
    public class ListTitleChanged : ListEvent
    {
        public ListTitleChanged(string title)
        {
            Title = title;
        }

        public string Title { get; private set; }

        public override string ToString()
        {
            return string.Format("List title changed to '{0}'", Title);
        }
    }
}
