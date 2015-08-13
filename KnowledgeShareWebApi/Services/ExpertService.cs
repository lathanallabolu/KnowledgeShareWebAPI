using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using KnowledgeShareWebApi.Models;

namespace KnowledgeShareWebApi.Services
{
    public class ExpertService
    {
        private readonly SqlConnection _connection;
        public ExpertService()
        {
            _connection = new SqlConnection("Data Source=b9wyaqyyrn.database.windows.net;Initial Catalog=MobileApp;User ID=laxmanrapolu;Password=Lucky_123;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            _connection.Open();
        }

        public IEnumerable<Courses> GetCourses(string zid)
        {
            var selectExpert = new SqlCommand(@"SELECT * FROM [dbo].[Ks_Expert] where ZID = @zid", _connection);
            selectExpert.Parameters.AddWithValue("@zid", zid);
            var expertObject = selectExpert.ExecuteReader();
            var courses = new List<Courses>();
            while (expertObject.Read())
            {
                var test = expertObject["Course"].ToString();
                var course = new Courses()
                {
                    Course = test
                };
                courses.Add(course);
            }
            _connection.Close();
            return courses;
        }

        public string AddExpert(Expert expert)
        {
            var checkExpert = GetCourses(expert.Zid);
            if (checkExpert.Any(course => course.Course == expert.Course))
            {
                return "You already enrolled for this course";
            }
            var insertExpert = new SqlCommand(@"INSERT INTO [dbo].[Ks_Expert] ([ZID], [Course], [First_Name], [Last_Name], [Email]) 
                                                   VALUES (@zid, @course, @firstMan, @lastName, @email)", _connection);
            insertExpert.Parameters.AddWithValue("@zid", expert.Zid == null ? DBNull.Value.ToString() : expert.Zid);
            insertExpert.Parameters.AddWithValue("@firstMan", expert.FirstName == null ? DBNull.Value.ToString() : expert.FirstName);
            insertExpert.Parameters.AddWithValue("@lastName", expert.LastName == null ? DBNull.Value.ToString() : expert.LastName);
            insertExpert.Parameters.AddWithValue("@email", expert.Email == null ? DBNull.Value.ToString() : expert.Email);
            insertExpert.Parameters.AddWithValue("@course", expert.Course == null ? DBNull.Value.ToString() : expert.Course);
            _connection.Open();
            insertExpert.ExecuteNonQuery();
            _connection.Close();
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