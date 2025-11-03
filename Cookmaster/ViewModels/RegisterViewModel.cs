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
        public List<string> SecurityQuestions { get; } = new List<string>
        {
            "What is your favorite color?",
            "What is your mother's maiden name?",
            "What is the name of your firrst pet?",
        };

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

            if (password != confirmPassword)
            {
                MessageBox.Show("The passwords doesn't match, please try again");
                return;
            }


            if (!ValidatePassword(password, confirmPassword))
            {
                MessageBox.Show("The password has to include at least 8 characters, 1 capital letter and 1 number");
                return;
            }

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(SelectedCountry) || string.IsNullOrWhiteSpace(SecurityQuestion) ||
                string.IsNullOrWhiteSpace(SecurityAnswer))
            {
                MessageBox.Show("Please fill out every field");
                return;
            }

            
            bool success = CreateUser(Username, password, SelectedCountry);

            if (success)
            {
                MessageBox.Show("Registration complete!");

                var mainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(App.GlobalUserManager)
                };
                mainWindow.Show();

                Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault()?.Close();
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
            if (password.Length < 8)
                return false;

            bool hasNbr = password.Any(char.IsDigit);
            bool hasUpper = password.Any(char.IsUpper);
            return hasNbr && hasUpper;
        }
        
       public bool CreateUser(string username, string password, string selectedCountry)
        {
           if (_userManager.FindUser(username) != null)
                return false;

            return _userManager.Register(username, password, selectedCountry, SecurityQuestion, SecurityAnswer); 
        }


    }
}
