using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GetReposList.Data;
using GetReposList.Queue;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GetReposList.Controllers
{
    [Route("api/[controller]")]
    public class RepositoriesController : Controller
    {
        private IQueueManager _queueManager;
        private IRepositoryStore _repositoryStore;

        public RepositoriesController(IQueueManager queueManager, IRepositoryStore repositoryStore)
        {
            _queueManager = queueManager;
            _repositoryStore = repositoryStore;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<RepositoryItem> Get()
        {
            _queueManager.PublishMessage("Repositories");

            return _repositoryStore.GetAll();
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
