using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using KnowledgeShareWebApi.Models;

namespace KnowledgeShareWebApi.Services
{
    public class ProblemService
    {
        private readonly SqlConnection _connection;
        private readonly ExpertService _expertService;
        public ProblemService()
        {
            _connection = new SqlConnection("Data Source=b9wyaqyyrn.database.windows.net;Initial Catalog=MobileApp;User ID=laxmanrapolu;Password=Lucky_123;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            _expertService=new ExpertService();
        }

        public List<Problem> GetExpertProblems(string zid)
        {
            var expertCourses = _expertService.GetCourses(zid);
            var problems = new List<Problem>();
            _connection.Open();
            foreach (var expertCourse in expertCourses)
            {
                var selectProblem = new SqlCommand(@"SELECT * FROM [dbo].[Ks_Problem] Where [Course] = @course", _connection);
                selectProblem.Parameters.AddWithValue("@course", expertCourse.Course);
                if(_connection.State == ConnectionState.Closed)
                _connection.Open();
                var problemObject = selectProblem.ExecuteReader();
                while (problemObject.Read())
                {
                    var problem = new Problem
                    {
                        Id = (int)problemObject["Id"],
                        FirstName = problemObject["First_Name"].ToString(),
                        LastName = problemObject["First_Name"].ToString(),
                        ProblemDescription = problemObject["Problem"].ToString(),
                        Solution = problemObject["Solution"].ToString(),
                        Email = problemObject["Email"].ToString(),
                        SolutionBy = problemObject["Solution_By"].ToString(),
                        Zid = problemObject["Zid"].ToString(),
                        Course = problemObject["Course"].ToString()
                    };
                    problems.Add(problem);
                }
                _connection.Close();
            }
            return problems;
        }

        public string AddProblem(Problem problem)
        {

            var insertSolution = new SqlCommand(@"INSERT INTO [dbo].[Ks_Problem] ([ZID], [Course], [First_Name], [Last_Name], [Email],
                                                  [Problem], [Solution], [Solution_By]) 
                                                   VALUES (@zid, @course, @firstMan, @lastName, @email, @problem, @solution, @solutionBy)", _connection);

            insertSolution.Parameters.AddWithValue("@zid", problem.Zid == null ? DBNull.Value.ToString() : problem.Zid);
            insertSolution.Parameters.AddWithValue("@course", problem.Course == null ? DBNull.Value.ToString() : problem.Course);
            insertSolution.Parameters.AddWithValue("@firstMan", problem.FirstName == null ? DBNull.Value.ToString() : problem.FirstName);
            insertSolution.Parameters.AddWithValue("@lastName", problem.LastName == null ? DBNull.Value.ToString() : problem.LastName);
            insertSolution.Parameters.AddWithValue("@email", problem.Email == null ? DBNull.Value.ToString() : problem.Email);
            insertSolution.Parameters.AddWithValue("@problem", problem.ProblemDescription == null ? DBNull.Value.ToString() : problem.ProblemDescription);
            insertSolution.Parameters.AddWithValue("@solution", "");
            insertSolution.Parameters.AddWithValue("@solutionBy", "");

            _connection.Open();
            insertSolution.ExecuteNonQuery();
            _connection.Close();
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