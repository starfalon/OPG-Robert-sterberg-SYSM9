using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookmaster.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Country { get; set; }

        //test
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public bool ValidateLogin(string username, string password)
        {
            return Username == username && Password == password;
        } 

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }

        public void UpdateDetails(string username, string country)
        {
            Username = username;
            Country = country;
        }

        

    }
}
