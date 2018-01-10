using Prism.Commands;
using Surveys.Core.ServiceInterfaces;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using Surveys.Entities;

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

            /* Envía las encuestas */
            var allSurveys = await localDbService.GetAllSurveysAsync();
            List<Survey> listOfSurveys = new List<Survey>(allSurveys);
            if(allSurveys != null && listOfSurveys.Count > 0)
            {
                await webApiService.SaveSurveysAsync(allSurveys);
                await localDbService.DeleteAllTeamsAsync();
            }

            /* Consulta los equipos */
            var allTeams = await webApiService.GetTeamsAsync();
            List<Survey> listOfTeams = new List<Survey>(allSurveys);
            if (allTeams != null && listOfTeams.Count > 0)
            {
                await localDbService.DeleteAllTeamsAsync();
                await localDbService.InsertTeamsAsync(allTeams);
            }

            Application.Current.Properties["lastSync"] = DateTime.Now;
            await Application.Current.SavePropertiesAsync();

            Status = $"Encuestas {allSurveys.GetType().ToString()} | Equipos {allTeams.GetType().ToString()}";
            //Status = $"Se enviaron {allSurveys.Count} encuestas y se obtuvieron {allTeams.Count} equipos";
            IsBusy = false;
        }
    }
}
