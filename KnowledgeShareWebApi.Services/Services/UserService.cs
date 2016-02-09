using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using KnowledgeShare.Models.Models;
using System.Data.SqlClient;
using KnowledgeShare.Repositories.Repositories;

namespace KnowledgeShareWebApi.Services.Services
{

    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService()
        {
           _userRepository = new UserRepository();
            
        }
        public User GetUser(string zid)
        {
            return _userRepository.GetUser(zid);
        }

        public string AddUser(User user)
        {
            var checkUser = _userRepository.GetUser(user.Zid);
            if (checkUser == null)
                return "User Already Exists";
            _userRepository.AddUser(user);
            try
            {
                SendEmail(user);
            }
            catch (Exception ex)
            {
                
               Console.WriteLine(ex);
            }
            return "Successfully Added User";
        }

        private static void SendEmail(User user)
        {
            var from = new MailAddress("lucky.aisu@gmail.com", "KnowledgeShare");
            var to = new MailAddress(user.Email);
            var mail = new MailMessage(from, to)
            {
                Subject = "Welcome To KnowledgeShare",
                Body = "You have been successfully registered in Knowledge Share"
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