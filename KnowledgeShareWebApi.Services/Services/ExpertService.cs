using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using KnowledgeShare.Models.Models;
using KnowledgeShare.Repositories.Repositories;

namespace KnowledgeShareWebApi.Services.Services
{
    public class ExpertService
    {
        private readonly ExpertRepository _expertRepository;
        public ExpertService()
        {
           _expertRepository = new ExpertRepository();
        }

        public IEnumerable<Courses> GetCourses(string zid)
        {
           return _expertRepository.GetCourses(zid);
        }

        public string AddExpert(Expert expert)
        {
            var checkExpert = _expertRepository.GetCourses(expert.Zid);
            if (checkExpert.Any(course => course.Course == expert.Course))
            {
                return "You already enrolled for this course";
            }

            _expertRepository.AddExpert(expert);

            try
            {
                SendEmail(expert);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex);
            }
            
            return "Successfully Enrolled As Expert";
        }

        private static void SendEmail(Expert expert)
        {
            var from = new MailAddress("lucky.aisu@gmail.com", "KnowledgeShare");
            var to = new MailAddress(expert.Email);
            var mail = new MailMessage(from, to)
            {
                Subject = "Enrolled as an Expert",
                Body = "You have been successfully enrolled as an expert for course" + " " + expert.Course
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