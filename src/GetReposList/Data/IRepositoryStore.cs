using System;
using System.Collections.Generic;

namespace GetReposList.Data
{
    public interface IRepositoryStore
    {
        void Clear();
        void Add(RepositoryItem item);
        IEnumerable<RepositoryItem> GetAll();
    }
}
