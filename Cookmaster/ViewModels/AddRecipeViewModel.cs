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
    class AddRecipeViewModel : ViewModelBase
    {
        private readonly RecipeManager _recipeManager;
        private readonly UserManager _userManager;
        private readonly Window _window;

        public string Title { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        

        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public AddRecipeViewModel(RecipeManager recipeManager, UserManager userManager, Window window)
        {
            _recipeManager = recipeManager;
            _userManager = userManager;
            _window = window;

            SaveCommand = new RelayCommand(_ => SaveRecipe());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void SaveRecipe()
        {
            if (string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Ingredients) ||
                string.IsNullOrWhiteSpace(Instructions) ||
                Date == default)
            {
                MessageBox.Show("Please fill out all spaces before saving!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var currentUser = _userManager.GetLoggedIn();
            var newRecipe = new Recipe
            {
                Title = Title,
                Category = Category,
                Ingredients = Ingredients,
                Instructions = Instructions,
                Date = Date,
                CreatedBy = currentUser.Username //_userManager.GetLoggedIn()?.Username ?? "Unknown"
            };

            _recipeManager.AddRecipe(newRecipe);

            MessageBox.Show("Recipe saved!");

            
            //var listWindow = new RecipeListWindow(_userManager, _recipeManager);
            //listWindow.Show();
            _window.Close();
            return;
        }

        private void Cancel()
        {
            _window.Close();
        }
    }
}
