using Cookmaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookmaster.Managers
{
    public class UserManager
    {
        private List<User> users = new List<User>();
        private User loggedIn;

        public bool Login(string username, string password)
        {
            
            foreach (User u in users)
            {
                if (u.Username == username && u.Password == password)
                {
                    loggedIn = u;
                    return true;
                }
            }
            return false;
        }

        public bool Register(string username, string password, string country, string question, string answer)
        {
            foreach (var user in users)
            {
                if (user.Username == username)
                {
                    return false;
                }
            }

            users.Add(new User { Username = username, Password = password, Country = country, SecurityQuestion = question, SecurityAnswer = answer });
            return true;
        }

        public User FindUser(string username)  //letar upp users i listan user
        {
            foreach (var user in users)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }
            return null; 
        }

        public void ChangePassword(string username, string newPassword)
        {
            foreach (var user in users)
            {
                if (user.Username == username)
                {
                    user.ChangePassword(newPassword);
                    return;
                }
            }
        }

        public User GetLoggedIn() //loggedin = currentuser
        {
            return loggedIn;
        }

        public bool ResetPassword(string username, string securityAnswer, string newPassword)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null) return false;


            if (!string.Equals(user.SecurityAnswer, securityAnswer, StringComparison.OrdinalIgnoreCase))
                return false;

            user.Password = newPassword;
            return true;
        }
    }

}
