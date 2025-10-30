using Cookmaster.Managers;
using Cookmaster.MVVM;
using Cookmaster.Views;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cookmaster.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;

        public string Username { get; set; }
        public string SelectedCountry { get; set; }

        public List<string> Countries { get; } = new List<string>
        {
            "Latveria", "Wakanda", "Sokovia", "Madripoor", "Genosha"
        };

        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public RelayCommand RegisterCommand { get; }
        public RegisterViewModel(UserManager manager)
        {
            _userManager = manager;
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register(object parameter)
        {
            var window = Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault();
            if (window == null) return;

            var passwordBox = window.FindName("PasswordBox") as System.Windows.Controls.PasswordBox;
            var confirmBox = window.FindName("ConfirmBox") as System.Windows.Controls.PasswordBox;

            string password = passwordBox?.Password ?? "";
            string confirmPassword = confirmBox?.Password ?? "";

            
            if (!ValidatePassword(password, confirmPassword))
            {
                MessageBox.Show("The password doesn't match, and has to include at least 6 characters, 1 capital letter and one number :)");
                return;
            }

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(SelectedCountry))
            {
                MessageBox.Show("Please fill out every field");
                return;
            }

            
            bool success = CreateUser(Username, password, SelectedCountry);

            if (success)
            {
                MessageBox.Show("Registration complete!");
                window.Close();
            }
            else
            {
                MessageBox.Show("This username is already in use!");
            }
        }

        private bool ValidatePassword(string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
                return false;
            if (password.Length < 6)
                return false;

            bool hasNbr = password.Any(char.IsDigit);
            bool hasUpper = password.Any(char.IsUpper);
            return hasNbr && hasUpper;
        }
        
        bool CreateUser(string username, string password, string selectedCountry)
        {
           if (_userManager.FindUser(username) != null)
                return false;

            return _userManager.Register(username, password, selectedCountry, SecurityQuestion, SecurityAnswer); 
        }


    }
}
