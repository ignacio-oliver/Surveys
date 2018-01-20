using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Core.ServiceInterfaces;
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
        private IWebApiService webApiService = null;
        private INavigationService navigationService = null;
        private IPageDialogService pageDialogservice = null;

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
        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy == value)
                {
                    return;
                }
                isBusy = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(IWebApiService webApiService, INavigationService navigationService, IPageDialogService pageDialogservice)
        {
            this.webApiService = webApiService;
            this.navigationService = navigationService;
            this.pageDialogservice = pageDialogservice;
            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCommandCanExecute).ObservesProperty(() => Username).ObservesProperty(() => Password);
        }

        private async void LoginCommandExecute()
        {
            IsBusy = true;
            try
            {
                var loginResult = await webApiService.LoginAsync(Username, Password);
                if (loginResult)
                {
                    await navigationService.NavigateAsync($"app:///{nameof(MainView)}/{nameof(RootNavigationView)}/{nameof(SurveysView)}");
                }
                else
                {
                    await pageDialogservice.DisplayAlertAsync(Literals.LoginTitle, Literals.AccessDenied, Literals.Ok);
                }
            }
            catch (Exception e)
            {
                await pageDialogservice.DisplayAlertAsync(Literals.LoginTitle, e.Message, Literals.Ok);
            }
            IsBusy = false;
        }

        private bool LoginCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
