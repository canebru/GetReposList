using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GetReposList.Queue;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GetReposList.Controllers
{
    [Route("api/[controller]")]
    public class LoadDataController : Controller
    {
        private IQueueManager _queueManager;

        public LoadDataController(IQueueManager queueManager)
        {
            _queueManager = queueManager;
        }
        
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _queueManager.PublishMessage("LoadData");
            return new List<string>();
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
