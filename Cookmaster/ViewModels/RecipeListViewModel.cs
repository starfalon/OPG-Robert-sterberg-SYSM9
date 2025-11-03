using Cookmaster.Managers;
using Cookmaster.Models;
using Cookmaster.MVVM;
using Cookmaster.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cookmaster.ViewModels
{
    public class RecipeListViewModel : ViewModelBase 
    {

        private readonly RecipeManager _recipeManager;

        private readonly UserManager _userManager;

        public ObservableCollection<Recipe> Recipes { get; set; }
        public Recipe SelectedRecipe { get; set; }
        public string SearchText { get; set; }
        public string SelectedCategory { get; set; }
        public ObservableCollection<string> Categories { get; set; }
        
        public string LoggedInUsername => _userManager.GetLoggedIn()?.Username ?? " ";

        public RelayCommand AddRecipeCommand { get; }
        public RelayCommand ViewDetailsCommand { get; }
        public RelayCommand RemoveRecipeCommand { get; }
        public RelayCommand FilterCommand { get; }
        public RelayCommand SignOutCommand { get; }
        public RelayCommand ShowInfoCommand { get; }
        public RelayCommand OpenUserDetailsCommand { get; }


        public RecipeListViewModel(UserManager usermanager, RecipeManager recipemanager)
        {
            _userManager = usermanager;
            _recipeManager = recipemanager;

            Recipes = new ObservableCollection<Recipe>(_recipeManager.GetAllRecipes());
            Categories = new ObservableCollection<string>(_recipeManager.GetAllCategories());

            AddRecipeCommand = new RelayCommand(AddRecipe);
            ViewDetailsCommand = new RelayCommand(ViewDetails);
            RemoveRecipeCommand = new RelayCommand(RemoveRecipe);
            FilterCommand = new RelayCommand(FilterRecipes);
            SignOutCommand = new RelayCommand(SignOut);
           
            ShowInfoCommand = new RelayCommand(ShowInfo);
            //OpenUserDetailsCommand = new RelayCommand(OpenUserDetails);
        }

        private void AddRecipe(object parameter)
        {
            //var addWindow = new AddRecipeWindow(_userManager, _recipeManager);
            //addWindow.ShowDialog();

            RefreshRecipes();
        }

        private void RemoveRecipe(object parameter)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Please select a recipe");
                return;
            }

            var currentUser = _userManager.GetLoggedIn();

            
            if (currentUser is AdminUser || SelectedRecipe.CreatedBy == currentUser.Username)
            {
                _recipeManager.RemoveRecipe(SelectedRecipe);
                RefreshRecipes();
            }
            else
            {
                MessageBox.Show("You can only remove your own recipes");
            }
        }

        private void ViewDetails(object parameter)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Please select a recipe");
                return;
            }

            var detailsWindow = new RecipeDetailWindow(SelectedRecipe, _recipeManager, _userManager);
            detailsWindow.Show();
        }

        private void FilterRecipes(object parameter)
        {
            var filtered = _recipeManager.Filter(SearchText);

            if (!string.IsNullOrWhiteSpace(SelectedCategory))
                filtered = filtered.Where(r => r.Category == SelectedCategory).ToList();

            Recipes = new ObservableCollection<Recipe>(filtered);
            OnPropertyChanged(nameof(Recipes));
        }

        private void SignOut(object parameter)
        {
            var loginWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(App.GlobalUserManager)
            };
            loginWindow.Show();

            Application.Current.Windows.OfType<RecipeListWindow>().FirstOrDefault()?.Close();
        }

        private void ShowInfo(object parameter)
        {
            MessageBox.Show(
                "CookMaster share and save all your favorite recipes!\n\n" +
                "• Click 'Add Recipe' to store a new recipe.\n" +
                "• Click 'Details' fpr more information about your recipe.\n" +
                "• Click User Details to see information about your account",
                "About CookMaster", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RefreshRecipes()
        {
            Recipes = new ObservableCollection<Recipe>(_recipeManager.GetAllRecipes());
            Categories = new ObservableCollection<string>(_recipeManager.GetAllCategories());
            OnPropertyChanged(nameof(Recipes));
            OnPropertyChanged(nameof(Categories));
        }


    }
}
