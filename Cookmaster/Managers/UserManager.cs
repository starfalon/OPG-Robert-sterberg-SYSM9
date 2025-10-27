using Cookmaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookmaster.Managers
{
    class UserManager
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

        public bool Register(string username, string password, string country)
        {
            foreach (var user in users)
            {
                if (user.Username == username)
                {
                    return false;
                }
            }

            users.Add(new User { Username = username, Password = password, Country = country });
            return true;
        }




    }

}
