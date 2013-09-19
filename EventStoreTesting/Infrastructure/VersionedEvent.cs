namespace EventStore.Infrastructure
{
    public abstract class VersionedEvent : IVersionedEvent
    {
        public string SourceId { get; set; }

        public int Version { get; set; }
    }
}
