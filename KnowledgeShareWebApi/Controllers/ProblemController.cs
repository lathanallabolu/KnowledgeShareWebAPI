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
    public class ProblemController : ApiController
    {
        private readonly ProblemService _problemService;
        public ProblemController()
        {
            _problemService = new ProblemService();
        }
        // GET api/problem
        public IEnumerable<Problem> Get()
        {
            return new List<Problem>();
        }

        // GET api/problem/5
        public IEnumerable<Problem> Get(string id)
        {
            return _problemService.GetExpertProblems(id);
        }

        // POST api/problem
        public string Post([FromBody]Problem problem)
        {
            return _problemService.AddProblem(problem);
           
        }

        // PUT api/problem/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/problem/5
        public void Delete(int id)
        {
        }
    }
}
