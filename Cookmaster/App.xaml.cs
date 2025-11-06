using Cookmaster.Managers;
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

            GlobalRecipeManager = new RecipeManager();
            GlobalUserManager = new UserManager(GlobalRecipeManager);

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(GlobalUserManager)
            };

            mainWindow.Show();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
        }
    }
}
