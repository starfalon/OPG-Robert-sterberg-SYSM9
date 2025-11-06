using Cookmaster.Managers;
using Cookmaster.Models;
using Cookmaster.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cookmaster.Views
{
    /// <summary>
    /// Interaction logic for RecipeDetailWindow.xaml
    /// </summary>
    public partial class RecipeDetailWindow : Window
    {
        public RecipeDetailWindow(Recipe recipe, RecipeManager manager, UserManager userManager)
        {
            InitializeComponent();
            DataContext = DataContext = new RecipeDetailViewModel(recipe, manager, userManager, this); ;
        }
     
    }
}
