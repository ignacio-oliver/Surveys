using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surveys.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;

        #region Properties
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if(username == value)
                {
                    return;
                }
                username = value;
                RaisePropertyChanged();
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if(password == value)
                {
                    return;
                }
                password = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCommandCanExecute).ObservesProperty(() => Username).ObservesProperty(() => Password);
        }

        private async void LoginCommandExecute()
        {
            await navigationService.NavigateAsync($"{nameof(MainView)}/{nameof(RootNavigationView)}/{nameof(SurveysView)}");
        }

        private bool LoginCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
