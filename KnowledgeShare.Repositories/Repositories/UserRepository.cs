using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using KnowledgeShare.Models.Models;
using System.Data.SqlClient;

namespace KnowledgeShare.Repositories.Repositories
{

    public class UserRepository
    {
        private readonly SqlConnection _connection;
        public UserRepository()
        {
            _connection = new SqlConnection("Data Source=b9wyaqyyrn.database.windows.net;Initial Catalog=MobileApp;User ID=laxmanrapolu;Password=Lucky_123;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            
        }
        public User GetUser(string zid)
        {
            var selectUser = new SqlCommand(@"SELECT * FROM [dbo].[Ks_User] where ZID = @zid", _connection);
            selectUser.Parameters.AddWithValue("@zid", zid);
            _connection.Open();
            var userObject = selectUser.ExecuteReader();
            var user = new User();
            
            while (userObject.Read())
            {
                user = new User()
                {
                    Zid = userObject["ZID"].ToString(),
                    Password = userObject["Password"].ToString(),
                    FirstName = userObject["First_Name"].ToString(),
                    LastName = userObject["Last_Name"].ToString(),
                    Email = userObject["Email"].ToString()
                }; 
            }
            _connection.Close();
            return user;
        }

        public string AddUser(User user)
        {
            var insertUser = new SqlCommand(@"INSERT INTO [dbo].[Ks_User] ([ZID], [First_Name], [Last_Name], [Email],[Password]) 
                                                   VALUES (@zid, @firstMan, @lastName, @email,@password)", _connection);
            insertUser.Parameters.AddWithValue("@zid", user.Zid);
            insertUser.Parameters.AddWithValue("@firstMan", user.FirstName);
            insertUser.Parameters.AddWithValue("@lastName", user.LastName);
            insertUser.Parameters.AddWithValue("@email", user.Email);
            insertUser.Parameters.AddWithValue("@password", user.Password);
            _connection.Open();
            insertUser.ExecuteNonQuery();
            _connection.Close();
            return "Successfully Added User";
        }
    }
}