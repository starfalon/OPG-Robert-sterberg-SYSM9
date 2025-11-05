using Cookmaster.Managers;
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
   public class UserDetailsViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        private readonly Window _window;

        public string CurrentUsername { get; set; }
        public string CurrentCountry { get; set; }
        public string NewUsername { get; set; }
        public string SelectedCountry { get; set; }

        public List<string> Countries { get; } = new List<string>
        {
            "Latveria", "Wakanda", "Sokovia", "Madripoor", "Genosha"
        };

        public RelayCommand SaveCommand {  get; set; }
        public RelayCommand CancelCommand { get; set; }


        public UserDetailsViewModel(UserManager manager, Window window)
        {
            _userManager = manager;
            _window = window;

            var currentUser = _userManager.GetLoggedIn();
            if (currentUser != null)
            {
                CurrentUsername = currentUser.Username;
                CurrentCountry = currentUser.Country;
                SelectedCountry = currentUser.Country;
            }

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(Cancel);

        }

        private void SaveChanges(object parameter)
        {
            var currentUser = _userManager.GetLoggedIn();
            var window = _window as UserDetailsWindow;
            var passwordBox = window.FindName("PasswordBox") as System.Windows.Controls.PasswordBox;
            var confirmBox = window.FindName("ConfirmPasswordBox") as System.Windows.Controls.PasswordBox;

            string newPassword = passwordBox?.Password ?? "";
            string confirmPassword = confirmBox?.Password ?? "";

            if (!string.IsNullOrWhiteSpace(NewUsername))
            {
                if (NewUsername.Length < 3)
                {
                    MessageBox.Show("Username must be at least 3 characters");
                    return;
                }

                if (_userManager.FindUser(NewUsername) != null && NewUsername != CurrentUsername)
                {
                    MessageBox.Show("Username is taken, please select another");
                    return;
                }

                currentUser.Username = NewUsername;
            }

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (newPassword.Length < 5)
                {
                    MessageBox.Show("Password must be at least 5 characters");
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("The passwords do not match");
                    return;
                }

                currentUser.Password = newPassword;
            }

            currentUser.Country = SelectedCountry;

            MessageBox.Show("User details have been updated");
            var recipeListWindow = Application.Current.Windows.OfType<RecipeListWindow>().FirstOrDefault();
            if (recipeListWindow?.DataContext is RecipeListViewModel recipeListVM)
            {
                recipeListVM.RefreshLoggedInUser();
            }
            _window.Close();
        }

        private void Cancel(object parameter)
        {
            _window.Close();
        }
    }
}
