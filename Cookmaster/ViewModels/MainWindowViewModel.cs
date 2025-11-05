using Cookmaster.Managers;
using Cookmaster.MVVM;
using Cookmaster.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cookmaster.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        public string Username { get; set; }

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand OpenRegisterCommand { get; }
        public RelayCommand ForgotPasswordCommand { get;set; }

        public MainWindowViewModel(UserManager userManager) 
        {
            
            LoginCommand = new RelayCommand(Login);
            OpenRegisterCommand = new RelayCommand(OpenRegister);
            ForgotPasswordCommand = new RelayCommand(OpenForgotPassword);
            _userManager = userManager;
        }
        private void Login(object parameter)
        {
            
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            var password = passwordBox?.Password ?? "";



            if (_userManager.Login(Username, password))
            {

               TwoFactorWindow twoFactorWindow = new TwoFactorWindow(OnTwoFactorSuccess);
                twoFactorWindow.ShowDialog();

                //var recipeWindow = new RecipeListWindow(_userManager, App.GlobalRecipeManager);
                //recipeWindow.Show();

                //Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }
        
        private void OpenRegister(object parameter)
        {
            
            var register = new RegisterWindow(App.GlobalUserManager);
            register.Show();
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
        }
        private void OpenForgotPassword(object _)
        {
            var forgot = new ForgotPasswordWindow(_userManager);
            
            forgot.Show();
        }

        private void OnTwoFactorSuccess()
        {
            
            var recipeWindow = new RecipeListWindow(_userManager, App.GlobalRecipeManager);
            recipeWindow.Show();

            
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
        }

    }
}
