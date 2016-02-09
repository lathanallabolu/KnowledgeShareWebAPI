using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using KnowledgeShare.Models.Models;

namespace KnowledgeShare.Repositories.Repositories
{
    public class ExpertRepository
    {
        private readonly SqlConnection _connection;
        public ExpertRepository()
        {
            _connection = new SqlConnection("Data Source=b9wyaqyyrn.database.windows.net;Initial Catalog=MobileApp;User ID=laxmanrapolu;Password=Lucky_123;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
           
        }

        public IEnumerable<Courses> GetCourses(string zid)
        {
            var selectExpert = new SqlCommand(@"SELECT * FROM [dbo].[Ks_Expert] where ZID = @zid", _connection);
            selectExpert.Parameters.AddWithValue("@zid", zid);
            _connection.Open();
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

        public void AddExpert(Expert expert)
        {
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
        }
    }
}