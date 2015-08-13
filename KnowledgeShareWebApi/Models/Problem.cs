using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeShareWebApi.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string ProblemDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Zid { get; set; }
        public string Email { get; set; }
        public string Solution { get; set; }
        public string SolutionBy { get; set; }
        public string Course { get; set; }
    }
}