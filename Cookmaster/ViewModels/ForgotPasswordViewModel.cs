using Cookmaster.Managers;
using Cookmaster.Models;
using Cookmaster.MVVM;
using Cookmaster.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cookmaster.ViewModels
{
   public class ForgotPasswordViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;

        public string Username { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public RelayCommand LoadSecurityQuestionCommand { get; }
        public RelayCommand ResetPasswordCommand { get; }

        public ForgotPasswordViewModel(UserManager userManager)
        {
            _userManager = userManager;
            LoadSecurityQuestionCommand = new RelayCommand(LoadSecurityQuestion);
            ResetPasswordCommand = new RelayCommand(ResetPassword);
        }

        private void LoadSecurityQuestion(object parameter)
        {
            var user = _userManager.FindUser(Username);
            if (user == null)
            {
                MessageBox.Show("User cannot be found");
                return;
            }

            SecurityQuestion = user.SecurityQuestion;
            OnPropertyChanged(nameof(SecurityQuestion));
        }

        private void ResetPassword(object parameter)
        {
            var window = Application.Current.Windows.OfType<ForgotPasswordWindow>().FirstOrDefault();
            if (window == null) return;

            var newPassBox = window.FindName("NewPasswordBox") as System.Windows.Controls.PasswordBox;
            string newPassword = newPassBox?.Password ?? "";

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(SecurityAnswer) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Please fill out every field");
                return;
            }

            bool success = _userManager.ResetPassword(Username, SecurityAnswer, newPassword);

            if (success)
            {
                MessageBox.Show("Password changed!");
                window.Close();

                var loginWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(_userManager)
                };
                loginWindow.Show();
            }
            else
            {
                MessageBox.Show("Invalid answer to security question");
            }
        }
    }
}
