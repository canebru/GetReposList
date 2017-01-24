using System.Collections.Generic;
using GetReposList.Data;

namespace GetReposList.Database
{
    public interface IDatabaseManager
    {
        List<RepositoryItem> GetItems();
        void InsertRow(int id, string name, string fullName);
    }
}
