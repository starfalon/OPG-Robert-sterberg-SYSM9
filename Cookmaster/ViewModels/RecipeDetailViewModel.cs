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
   public class RecipeDetailViewModel : ViewModelBase

    {
        private readonly RecipeManager _recipeManager;
        private readonly UserManager _userManager;
        private readonly Window _window;

        public Recipe EditableRecipe { get; set; }
        public bool IsLocked { get; set; } = true;
        public bool IsEditing => !IsLocked;

        public RelayCommand EditCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand CopyCommand { get; }
        public RelayCommand CloseCommand { get; }

        public RecipeDetailViewModel(Recipe recipe, RecipeManager recipeManager, UserManager userManager, Window window)
        {
            _recipeManager = recipeManager;
            _userManager = userManager;
            _window = window;

            
            EditableRecipe = new Recipe
            {
                Title = recipe.Title,
                Category = recipe.Category,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                CreatedBy = recipe.CreatedBy,
                Date = recipe.Date
            };

            EditCommand = new RelayCommand(_ => UnlockFields());
            SaveCommand = new RelayCommand(_ => SaveChanges());
            CopyCommand = new RelayCommand(_ => CopyRecipe());
            CloseCommand = new RelayCommand(_ => Close());
        }

        private void UnlockFields()
        {
            IsLocked = false;
            OnPropertyChanged(nameof(IsLocked));
            OnPropertyChanged(nameof(IsEditing));
        }

        private void SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(EditableRecipe.Title) ||
                string.IsNullOrWhiteSpace(EditableRecipe.Ingredients) ||
                string.IsNullOrWhiteSpace(EditableRecipe.Instructions))
            {
                MessageBox.Show("Please fill out all fields");
                return;
            }

            _recipeManager.UpdateRecipe(EditableRecipe);

            MessageBox.Show("Recipe saved");

            _window.Close();
        }

        private void CopyRecipe()
        {
            var currentUser = _userManager.GetLoggedIn();

            var newRecipe = new Recipe
            {
                Title = EditableRecipe.Title + " (copy)",
                Category = EditableRecipe.Category,
                Ingredients = EditableRecipe.Ingredients,
                Instructions = EditableRecipe.Instructions,
                CreatedBy = currentUser?.Username ?? "Unknown",
                Date = DateTime.Now
            };

            _recipeManager.AddRecipe(newRecipe);

            MessageBox.Show("Recipe copied");

            _window.Close();
        }

        private void Close()
        {
            _window.Close();
        }
    }
}
