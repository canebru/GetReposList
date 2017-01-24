using System.Collections.Generic;
using GetReposList.Data;
using GetReposList.Database;
using GetReposList.HttpWeb;
using Newtonsoft.Json;

namespace GetReposList.Queue
{
    public class MessageHandler : IMessageHandler
    {
        private IDatabaseManager _databaseManager;
        private IRepositoryStore _store;
        private IHttpManager _httpManager;

        public MessageHandler(
            IDatabaseManager manager,
            IRepositoryStore store,
            IHttpManager httpManager)
        {
            _databaseManager = manager;
            _store = store;
            _httpManager = httpManager;
        }

        public void Handle(string message)
        {
            switch (message)
            {
                case "LoadData":
                    LoadData();
                    break;
                case "Repositories":
                    ReadData();
                    break;
                default:
                    break;
            }
        }

        private void LoadData()
        {
            var repositoryData = _httpManager.PullData();
            var items = ParseData(repositoryData);
            foreach (var item in items)
            {
                _databaseManager.InsertRow(item.Id, item.Name, item.FullName);
            }
        }

        private List<RepositoryItem> ParseData(string repositoryData)
        {
            var repositories = JsonConvert.DeserializeObject<List<ReposJson>>(repositoryData);

            var items = new List<RepositoryItem>();
            foreach (var repository in repositories)
            {
                items.Add(new RepositoryItem{
                    Id = repository.id,
                    FullName = repository.full_name,
                    Name = repository.name,
                    Owner = repository.owner.login
                });
            }
            
            return items;
        }

        private void ReadData()
        {
            var items = _databaseManager.GetItems();

            _store.Clear();
            foreach (var item in items)
            {
                _store.Add(item);
            }
        }
    }
}
