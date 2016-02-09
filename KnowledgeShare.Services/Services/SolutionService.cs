using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using KnowledgeShare.Models.Models;
using KnowledgeShare.Repositories.Repositories;

namespace KnowledgeShare.Services.Services
{
    public class SolutionService
    {
        private readonly SolutionRepository _solutionRepository;
        public SolutionService()
        {
            _solutionRepository = new SolutionRepository();
        }
        public List<Problem> GetAllSolution(string zid)
        {
            return _solutionRepository.GetAllSolution(zid);
        }
        public List<Problem> GetSolution(int key)
        {
            return _solutionRepository.GetSolution(key);
        }

        public string AddSolution(Problem problem)
        {
            return _solutionRepository.AddSolution(problem);
        }
    }
}