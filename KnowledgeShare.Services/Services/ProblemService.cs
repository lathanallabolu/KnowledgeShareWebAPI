using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using KnowledgeShare.Models.Models;
using KnowledgeShare.Repositories.Repositories;

namespace KnowledgeShare.Services.Services
{
    public class ProblemService
    {
        private readonly ExpertRepository _expertRepository;
        private readonly ProblemRepository _problemRepository;
        public ProblemService()
        {
            _expertRepository = new ExpertRepository();
            _problemRepository = new ProblemRepository();
        }

        public List<Problem> GetExpertProblems(string zid)
        {
            var expertCourses = _expertRepository.GetCourses(zid);
            return _problemRepository.GetExpertProblems(expertCourses);
        }

        public string AddProblem(Problem problem)
        {
            _problemRepository.AddProblem(problem);
            try
            {
                SendEmail(problem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "Successfully Posted a Problem";
        }

        private static void SendEmail(Problem problem)
        {
            var from = new MailAddress("lucky.aisu@gmail.com", "KnowledgeShare");
            var to = new MailAddress(problem.Email);
            var mail = new MailMessage(from, to)
            {
                Subject = "Submitted a Problem",
                Body = "You have been successfully submitted a problem for course" + " " + problem.Course
            };

            var ms = new SmtpClient("smtpcorp.com")
            {
                Credentials = new NetworkCredential("lucky.aisu@gmail.com", "lucky_123"),
                Port = 2525
            };
            ms.Send(mail);
        }
    }
}