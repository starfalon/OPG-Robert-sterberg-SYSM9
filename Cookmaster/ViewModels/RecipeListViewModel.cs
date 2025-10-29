using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using Cookmaster.MVVM;
using Cookmaster.Models;
using Cookmaster.Managers;

namespace Cookmaster.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public Recipe SelectedRecipe { get; set; }
        public string SearchText { get; set; }
        public User CurrentUser { get; }

        private readonly RecipeManager _recipeManager;
        private readonly UserManager _userManager;


    }
}
