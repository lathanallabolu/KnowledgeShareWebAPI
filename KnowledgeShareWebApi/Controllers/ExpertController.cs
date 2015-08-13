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
    public class ExpertController : ApiController
    {
        private readonly ExpertService _expertService;
        public ExpertController()
        {
         _expertService = new ExpertService();   
        }
        // GET api/expert
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/expert/5
        public IEnumerable<Courses> Get(string id)
        {
           return _expertService.GetCourses(id);
        }

        // POST api/expert
        public string Post([FromBody]Expert expert)
        {
           return _expertService.AddExpert(expert);
        }

        // PUT api/expert/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/expert/5
        public void Delete(int id)
        {
        }
    }
}
