using Cookmaster.Managers;
using Cookmaster.Models;
using Cookmaster.ViewModels;
using Cookmaster.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Cookmaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserManager GlobalUserManager { get; private set; }
        public static RecipeManager GlobalRecipeManager { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GlobalUserManager = new UserManager();
            GlobalRecipeManager = new RecipeManager();

            PreloadRecipes();


            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(GlobalUserManager)
            };

            mainWindow.Show();

        }

        private void PreloadRecipes()
        {
            
            var admin = GlobalUserManager.FindUser("admin");
            var user = GlobalUserManager.FindUser("user");

            
            GlobalRecipeManager.AddRecipe(new Recipe
            {
                Title = "Wakandan Spiced Stew",
                Category = "African",
                Time = "3 hours +",
                Ingredients = "Meat, vegetables, spices",
                Instructions = "Bring to a boil and let simmer fpr three hours.",
                CreatedBy = user.Username,
                Date = DateTime.Now
            });

            
            GlobalRecipeManager.AddRecipe(new Recipe
            {
                Title = "Latverian Pancakes",
                Category = "Breakfast",
                Time = "25 minutes",
                Ingredients = "Flour, eggs, milk",
                Instructions = "In a high-heat pan, fry til golden brown.",
                CreatedBy = user.Username,
                Date = DateTime.Now
            });

            GlobalRecipeManager.AddRecipe(new Recipe
            {
                Title = "Swedish Stewed Macaroni",
                Category = "Swedish Husman",
                Time = "35 minutes",
                Ingredients = "Macaroni, milk, salt",
                Instructions = "Boil macaroni in milk for 20 mins",
                CreatedBy = user.Username,
                Date = DateTime.Today
            });
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
        }
    }

}
