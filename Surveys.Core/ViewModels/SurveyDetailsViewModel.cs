using Surveys.Entities;
using Surveys.Core.ServiceInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using Prism.Commands;
using Surveys.Core.Views;
using System.Collections.Generic;
using System.Diagnostics;

namespace Surveys.Core.ViewModels
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private ILocalDbService localDbService = null;
        private IEnumerable<Team> localDbTeams = null;

        #region Properties
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                {
                    return;
                }
                name = value;
                RaisePropertyChanged();
            }
        }

        private DateTime birthdate;
        public DateTime Birthdate
        {
            get
            {
                return birthdate;
            }
            set
            {
                if (birthdate == value)
                {
                    return;
                }
                birthdate = value;
                RaisePropertyChanged();
            }
        }

        private string team;
        public string Team
        {
            get
            {
                return team;
            }
            set
            {
                if (team == value)
                {
                    return;
                }
                team = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand SelectTeamCommand { get; set; }
        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, ILocalDbService localDbService = null)
        {
            this.navigationService = navigationService;
            this.localDbService = localDbService;

            /* Inicializo la fecha */
            Birthdate = DateTime.Today;
     
            SelectTeamCommand = new DelegateCommand(SelectTeamCommandExecute);
            EndSurveyCommand = new DelegateCommand(EndSurveyCommandExecute, EndSurveyCommandCanExecute)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => Team);
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            localDbTeams = await localDbService.GetAllTeamsAsync();
            if (parameters.ContainsKey("id"))
            {
                Team = localDbTeams.First(t => t.Id == (int)parameters["id"]).Name;
            }
        }

        private async void SelectTeamCommandExecute()
        {
            await navigationService.NavigateAsync(nameof(TeamSelectionView), useModalNavigation: true);
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Birthdate = Birthdate,
                TeamId = localDbTeams.First(t => t.Name == Team).Id
            };

            var geolocationService = Xamarin.Forms.DependencyService.Get<IGeolocationService>();

            if(geolocationService != null)
            {
                try
                {
                    var currentLocation = await geolocationService.GetCurrentLocationAsync();
                    newSurvey.Latitude = currentLocation.Item1;
                    newSurvey.Longitude = currentLocation.Item2;
                }
                catch (Exception)
                {
                    newSurvey.Latitude = 0;
                    newSurvey.Longitude = 0;
                }
            }

            await localDbService.InsertSurveyAsync(newSurvey);

            await navigationService.GoBackAsync();
        }

        private bool EndSurveyCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Team);
        }
    }
}
