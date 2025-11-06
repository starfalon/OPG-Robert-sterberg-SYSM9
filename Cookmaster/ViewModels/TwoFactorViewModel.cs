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
    public class TwoFactorViewModel : ViewModelBase
    {
        private readonly string _generatedCode;
        private readonly Window _window;
        private readonly Action _onSuccess;

        public string EnteredCode { get; set; }

        public RelayCommand VerifyCommand { get; private set; }
        
        public string GeneratedCode
        {
            get { return _generatedCode; }
        }
        public TwoFactorViewModel(Window window, Action onSuccess)
        {
            _window = window;
            _onSuccess = onSuccess;

            var rand = new Random();
            _generatedCode = rand.Next(100000, 999999).ToString();

           VerifyCommand = new RelayCommand(Verify);
        }

        private void Verify(object parameter)
        {
            if (EnteredCode == _generatedCode)
            {
                MessageBox.Show("Verified!");
                foreach (var win in Application.Current.Windows.OfType<TwoFactorCodeWindow>().ToList())
                {
                    win.Close();
                }
                _window.DialogResult = true;
                _window.Close();
            }
            else
            {
                MessageBox.Show("Wrong code, please try again");
            }
            
        }
    }
}
