using PersonnelAccountingSystem.Infrastructure.Commands;
using PersonnelAccountingSystem.Models;
using PersonnelAccountingSystem.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PersonnelAccountingSystem.ViewModels
{
    class LoginWindowViewModel : ViewModel
    {
        #region Commands
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CloseCanExecute(object o) => true;
        private void CloseOnExecuted(object o)
        {
            Application.Current.Shutdown();
        }
        #endregion CloseApplicationCommand

        #region AuthorizationCommand
        public ICommand AuthorizationCommand { get; }

        private string login;
        public string Login {
            get => login;
            set
            {
                login = value;
                OnPropertyChange();
            }
        }

        private string pass;
        public string Pass
        {
            get => pass;
            set
            {
                pass = value;
                OnPropertyChange();
            }
        }

        private bool AutorizationCanExecute(object o) 
        {
            var a = false;
            using (var context = new Personnel_Accounting_SystemEntities()) 
            {
                var data = context.USERS;

                foreach (var user in data)
                    if (Equals(user.Login, login) && Equals(user.Password, pass)) { a = true; };
            }
            return a;
        }

        private void AuthorizationExecuted(object o) 
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            App.Current.MainWindow.Close();
        }

        #endregion Authorization Command

        #endregion Commands


        public LoginWindowViewModel()
        {
            CloseApplicationCommand = new LyambdaCommand(CloseOnExecuted, CloseCanExecute);
            AuthorizationCommand = new LyambdaCommand(AuthorizationExecuted, AutorizationCanExecute);
        }
    }
}
