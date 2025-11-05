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
using System;
using System.Windows;
using Cookmaster.ViewModels;
namespace Cookmaster.Views
{
    /// <summary>
    /// Interaction logic for TwoFactorWindow.xaml
    /// </summary>
    public partial class TwoFactorWindow : Window
    {
        private readonly TwoFactorViewModel _vm;

        public TwoFactorWindow(Action onSuccess)
        {
            InitializeComponent();

            _vm = new TwoFactorViewModel(this, onSuccess);
            this.DataContext = _vm;   

            
            foreach (var win in Application.Current.Windows.OfType<TwoFactorCodeWindow>().ToList())
                win.Close();

            var codeWindow = new TwoFactorCodeWindow(_vm.GeneratedCode);
            codeWindow.Show();
        }
    }
}
