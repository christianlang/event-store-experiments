namespace EventStore.Events
{
    public class ListCreated : ListEvent
    {
        public override string ToString()
        {
            return string.Format("List '{0}' created.", SourceId);
        }
    }
}
