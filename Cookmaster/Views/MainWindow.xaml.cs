using Cookmaster.ViewModels;
using Cookmaster.Managers;
using System.Windows;

namespace Cookmaster.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var userManager = App.GlobalUserManager ?? new UserManager(App.GlobalRecipeManager ?? new RecipeManager());
            DataContext = new MainWindowViewModel(userManager);
        }

    }
}