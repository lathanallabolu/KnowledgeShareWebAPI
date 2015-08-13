using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnowledgeShareWebApi.Models;
using KnowledgeShareWebApi.Services;
namespace KnowledgeShareWebApi.Controllers
{
    public class SolutionController : ApiController
    {
        private readonly SolutionService _solutionService;
        public SolutionController()
        {
            _solutionService = new SolutionService();
        }
        // GET api/solution
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/solution/5
        public IEnumerable<Problem> Get(string id)
        {
            return _solutionService.GetAllSolution(id);
        }
        // GET api/solution/5
        public IEnumerable<Problem> Get(string id, int key)
        {
            return _solutionService.GetSolution(key);
        }

        // POST api/solution
        public string Post([FromBody]Problem problem)
        {
            return _solutionService.AddSolution(problem);
        }

        // PUT api/solution/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/solution/5
        public void Delete(int id)
        {
        }
    }
}
