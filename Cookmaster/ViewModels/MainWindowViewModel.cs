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
            // ✅ parameter är PasswordBox (skickas från XAML)
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            var password = passwordBox?.Password ?? "";

            if (_userManager.Login(Username, password))
            {
                //MessageBox.Show("Login successful!");

                var recipeWindow = new RecipeListWindow(_userManager, new RecipeManager());
                recipeWindow.Show();

                // Stäng loginfönstret
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }
        //private void Login(object parameter)
        //{
        //    var passwordBox = parameter as System.Windows.Controls.PasswordBox;
        //    var password = passwordBox?.Password ?? "";

        //    if (_userManager.Login(Username, password))
        //    {
        //        var recipeList = new RecipeListWindow(_userManager)
        //        {
        //            DataContext = new RecipeListWindow(_userManager)
        //        };
        //        recipeList.Show();
        //        Application.Current.Windows.OfType<MainWindow>().First().Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Wrong username or password.");
        //    }
        //}

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

    }
}
