using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeShareWebApi.Models
{
    public class Expert
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Zid { get; set; }
        public string Email { get; set; }
    }
}