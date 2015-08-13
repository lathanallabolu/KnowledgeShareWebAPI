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
    public class UserController : ApiController
    {
        private readonly UserService _service;
        public UserController()
        {
               _service = new UserService(); 
        }
        // GET api/user
        public User Get()
        {
            return new User();
        }

        // GET api/user/5
        public User Get(string id)
        {
            var user = _service.GetUser(id);
            return user;
        }

        // POST api/user
        public string Post([FromBody]User user)
        {
          return  _service.AddUser(user);
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
