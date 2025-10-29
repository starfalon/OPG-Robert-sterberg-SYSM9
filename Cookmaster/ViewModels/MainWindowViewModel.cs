using Cookmaster.Managers;
using Cookmaster.MVVM;
using Cookmaster.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cookmaster.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        public string Username { get; set; }

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand OpenRegisterCommand { get; set; }
        public RelayCommand ForgotPasswordCommand { get;set; }

        public MainWindowViewModel() 
        {
            _userManager = new UserManager();
            //LoginCommand = new RelayCommand(Login);
            //OpenRegisterCommand = new RelayCommand(OpenRegister);
            //ForgotPasswordCommand = new RelayCommand(OpenForgotPassword);
        }

        //private void Login(string password)
        //{
        //    if (_userManager.Login(Username, password))
        //    {
        //        var recipeList = new RecipeListWindow
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

        //private void OpenRegister(object _) 
        //{
        //    var register = new RegisterWindow
        //    {
        //        DataContext = new RegisterViewModel()
        //    };
        //    register.ShowDialog();
        //}
        //private void OpenForgotPassword(object _)
        //{
        //    var forgot = new ForgotPasswordWindow
        //    {
        //        DataContext = new ForgotPasswordViewModel(_userManager)
        //    };
        //}

    }
}
