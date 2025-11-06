using Cookmaster.ViewModels;
using System.Windows;

namespace Cookmaster.Views
{
    /// <summary>
    /// Interaction logic for RecipeListWindow.xaml
    /// </summary>
    public partial class RecipeListWindow : Window
    {
        public RecipeListWindow(Managers.UserManager userManager, Managers.RecipeManager recipeManager)
        {
            InitializeComponent();
            DataContext = new RecipeListViewModel(userManager, recipeManager);
        }
    }
}
