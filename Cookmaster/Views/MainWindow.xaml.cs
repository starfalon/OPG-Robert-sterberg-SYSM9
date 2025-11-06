using Cookmaster.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cookmaster.Models;
using Cookmaster.Managers;

namespace Cookmaster.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserManager _userManager = new UserManager(App.GlobalRecipeManager);
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(_userManager);
        }

    }
}