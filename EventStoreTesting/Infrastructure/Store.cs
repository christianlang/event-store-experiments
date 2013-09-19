using System.Collections.Generic;
using System.IO;

namespace EventStore.Infrastructure
{
    public class Store : IStore
    {
        private readonly string _path;
        private readonly ITextSerializer _serializer;
        private List<IVersionedEvent> _events;

        public Store(string path)
        {
            _path = path;
            _serializer = new JsonTextSerializer();

            if (File.Exists(path))
            {
                Load(path);
            }
            else
            {
                _events = new List<IVersionedEvent>();
            }
        }

        public IEnumerable<IVersionedEvent> Events
        {
            get { return _events; }
        }

        public void AddEvents(IEnumerable<IVersionedEvent> events)
        {
            _events.AddRange(events);
            Save();
        }

        private void Load(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                _events = (List<IVersionedEvent>)_serializer.Deserialize(new StreamReader(stream));
            }
        }

        private void Save()
        {
            using (var stream = File.OpenWrite(_path))
            {
                _serializer.Serialize(new StreamWriter(stream), _events);
            }
        }
    }
}
