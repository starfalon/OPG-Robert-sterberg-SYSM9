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
        private User? loggedIn;
        private RecipeManager _recipeManager;

        private static bool _seeded = false;

        public UserManager(RecipeManager recipeManager)
        {
            _recipeManager = recipeManager;

            var admin = new AdminUser
            {
                Username = "admin",
                Password = "password",
                Country = "Sokovia",
                SecurityQuestion = "What is your favorite color?",
                SecurityAnswer = "red"
            };

            var user = new User
            {
                Username = "user",
                Password = "password",
                Country = "Latveria",
                SecurityQuestion = "What is the name of your first pet?",
                SecurityAnswer = "göran"
            };

            var testUser = new User
            {
                Username = "test",
                Password = "test",
                Country = "Latveria",
                SecurityQuestion = "What is the name of your first pet?",
                SecurityAnswer = "göran"
            };

            users.Add(admin);
            users.Add(user);
            users.Add(testUser);

            if (_seeded) return;
            _seeded = true;


            _recipeManager.AddRecipe(new Recipe
            {
                Title = "Wakandan Spiced Stew",
                Category = "African",
                Ingredients = "Meat, vegetables, spices",
                Instructions = "Bring to a boil and let simmer for three hours.",
                CreatedBy = user.Username, 
                Date = DateTime.Now
            });

            _recipeManager.AddRecipe(new Recipe
            {
                Title = "Latverian Pancakes",
                Category = "Breakfast",
                Ingredients = "Flour, eggs, milk",
                Instructions = "In a high-heat pan, fry til golden brown.",
                CreatedBy = user?.Username ?? "system",
                Date = DateTime.Now
            });

            _recipeManager.AddRecipe(new Recipe
            {
                Title = "Swedish Stewed Macaroni",
                Category = "Swedish Husman",
                Ingredients = "Macaroni, milk, salt",
                Instructions = "Boil macaroni in milk for 20 mins",
                CreatedBy = user?.Username ?? "system",
                Date = DateTime.Today
            });
        }

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

        public User? FindUser(string username)  
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

        public User? GetLoggedIn() //loggedin = currentuser
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

        public void Logout()
        {
            loggedIn = null;
        }
    }

}
