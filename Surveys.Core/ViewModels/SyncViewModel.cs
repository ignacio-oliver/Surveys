using Prism.Commands;
using Surveys.Core.ServiceInterfaces;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;
using System;
using System.Linq;
using System.Diagnostics;

namespace Surveys.Core.ViewModels
{
    public class SyncViewModel : ViewModelBase
    {
        private IWebApiService webApiService = null;
        private ILocalDbService localDbService = null;

        #region Properties
        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if(status == value)
                {
                    return;
                }
                status = value;
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
                if(isBusy == value)
                {
                    return;
                }
                isBusy = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand SyncCommand { get; set; }

        public SyncViewModel(IWebApiService webApiService, ILocalDbService localDbService)
        {
            this.webApiService = webApiService;
            this.localDbService = localDbService;

            SyncCommand = new DelegateCommand(SyncCommandExecute);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (Application.Current.Properties.ContainsKey("lastSync"))
                Status = $"Última actualización: {(DateTime)Application.Current.Properties["lastSync"]}";
            else
                Status = "No se han sincronizado los datos";
        }

        private async void SyncCommandExecute()
        {
            IsBusy = true;

            int teamsCount = 0;
            int surveysCount = 0;

            /* Envía las encuestas */
            var allSurveys = await localDbService.GetAllSurveysAsync();
            if (allSurveys != null && allSurveys.Any())
            {
                surveysCount = allSurveys.Count();
                await webApiService.SaveSurveysAsync(allSurveys);
                await localDbService.DeleteAllSurveysAsync();
            }

            /* Consulta los equipos */
            var allTeams = await webApiService.GetTeamsAsync();
            if (allTeams != null && allTeams.Any())
            {
                teamsCount = allTeams.Count();
                await localDbService.DeleteAllTeamsAsync();
                await localDbService.InsertTeamsAsync(allTeams);
            }

            Application.Current.Properties["lastSync"] = DateTime.Now;
            await Application.Current.SavePropertiesAsync();

            if (allSurveys != null && allTeams != null)
            {
                Status = $"Se enviaron {surveysCount} encuestas y se obtuvieron {teamsCount} equipos";
            }
            else
            {
                Status = $"Error en la sincronización";
            }
            
            IsBusy = false;
        }
    }
}
