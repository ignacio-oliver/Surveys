﻿using Prism.Commands;
using Surveys.Core.ServiceInterfaces;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using Surveys.Entities;
using System.Collections.ObjectModel;

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

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (Application.Current.Properties.ContainsKey("lastSync"))
                Status = $"Última actualización: {(DateTime)Application.Current.Properties["lastSync"]}";
            else
                Status = "No se han sincronizado los datos";
        }

        private async void SyncCommandExecute()
        {
            IsBusy = true;

            /* Envía las encuestas */
            var allSurveys = (ICollection<Survey>) await localDbService.GetAllSurveysAsync();
            if(allSurveys != null && allSurveys.Count > 0)
            {
                await webApiService.SaveSurveysAsync(allSurveys);
                await localDbService.DeleteAllTeamsAsync();
            }

            /* Consulta los equipos */
            var allTeams = (ICollection<Team>) await webApiService.GetTeamsAsync();
            if(allTeams != null && allTeams.Count > 0)
            {
                await localDbService.DeleteAllTeamsAsync();
                await localDbService.InsertTeamsAsync(allTeams);
            }

            Application.Current.Properties["lastSync"] = DateTime.Now;
            await Application.Current.SavePropertiesAsync();

            Status = $"Se enviaron {allSurveys.Count} encuestas y se obtuvieron {allTeams.Count} equipos";
            IsBusy = false;
        }
    }
}
