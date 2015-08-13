using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.Web;
using KnowledgeShareWebApi.Models;

namespace KnowledgeShareWebApi.Services
{
    public class SolutionService
    {
        private readonly SqlConnection _connection;
        public SolutionService()
        {
            _connection = new SqlConnection("Data Source=b9wyaqyyrn.database.windows.net;Initial Catalog=MobileApp;User ID=laxmanrapolu;Password=Lucky_123;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
        }
        public List<Problem> GetAllSolution(string zid)
        {
           var selectProblem = new SqlCommand(@"SELECT * FROM [dbo].[Ks_Problem] Where [ZID] = @zid", _connection);
                selectProblem.Parameters.AddWithValue("@zid", zid);
           return GetSolutionsList(selectProblem);
        }
        public List<Problem> GetSolution(int key)
        {
            var selectProblem = new SqlCommand(@"SELECT * FROM [dbo].[Ks_Problem] Where [ID] = @id", _connection);
            selectProblem.Parameters.AddWithValue("@id", key);
            return GetSolutionsList(selectProblem);
        }

        private List<Problem> GetSolutionsList(SqlCommand selectProblem)
        {
            _connection.Open();
            var problemObject = selectProblem.ExecuteReader();
            var problems = new List<Problem>();
            while (problemObject.Read())
            {
                var problem = new Problem
                {
                    Id = (int) problemObject["Id"],
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
            return problems;
        }

        public string AddSolution(Problem problem)
        {

            var insertSolution = new SqlCommand(@"UPDATE [dbo].[Ks_Problem] SET [Solution] = @solution,
                                                [Solution_By] = @solutionBy Where [Id] = @id", _connection);
            insertSolution.Parameters.AddWithValue("@solution", problem.Solution);
            insertSolution.Parameters.AddWithValue("@solutionBy", problem.SolutionBy);
            insertSolution.Parameters.AddWithValue("@id", problem.Id);
            _connection.Open();
            insertSolution.ExecuteNonQuery();
            _connection.Close();
            try
            {
             //   SendEmail(problem);
            }
            catch (Exception ex)
            {
                
               Console.WriteLine(ex);
            }
            
            return "Successfully Posted a Solution";
        }

        private static void SendEmail(Problem problem)
        {
            var from = new MailAddress("lucky.aisu@gmail.com", "KnowledgeShare");
            var to = new MailAddress(problem.Email);
            var mail = new MailMessage(from, to)
            {
                Subject = "Solution has been provided to your problem",
                Body = problem.SolutionBy + " " + "has submitted a solution for your problem in course" + " " + problem.Course
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