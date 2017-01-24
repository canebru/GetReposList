using System.Collections.Generic;
using System.Collections.Concurrent;

namespace GetReposList.Data
{
    public class RepositoryStore : IRepositoryStore
    {
        private ConcurrentDictionary<int, RepositoryItem> _store;

        public RepositoryStore()
        {
            _store = new ConcurrentDictionary<int, RepositoryItem>();
        }

        public void Clear()
        {
            _store.Clear();
        }

        public void Add(RepositoryItem item)
        {
            _store.TryAdd(item.Id, item);
        }

        public IEnumerable<RepositoryItem> GetAll()
        {
            return _store.Values;
        }
    }
}
