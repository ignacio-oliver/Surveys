﻿using Surveys.Core.Models;
using Surveys.Core.ServiceInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using Prism.Commands;

namespace Surveys.Core.ViewModels
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private IPageDialogService pageDialogService = null;

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

        private string favoriteTeam;
        public string FavoriteTeam
        {
            get
            {
                return favoriteTeam;
            }
            set
            {
                if (favoriteTeam == value)
                {
                    return;
                }
                favoriteTeam = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> teams;
        public ObservableCollection<string> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                if (teams == value)
                {
                    return;
                }
                teams = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand SelectTeamCommand { get; set; }
        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;

            Teams = new ObservableCollection<string>(new[] 
            {
                "Alianza Lima", "América", "Boca Juniors", "Caracas FC", "Colo-Colo", "Nacional", "Real Madrid", "Saprissa"
            });

            SelectTeamCommand = new DelegateCommand(SelectTeamCommandExecute);
            EndSurveyCommand = new DelegateCommand(EndSurveyCommandExecute, EndSurveyCommandCanExecute)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => FavoriteTeam);
        }

        private async void SelectTeamCommandExecute()
        {
            var team = await pageDialogService.DisplayActionSheetAsync(Literals.FavoriteTeamTitle, null, null, Teams.ToArray());
            FavoriteTeam = team;
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Name = Name,
                Birthdate = Birthdate,
                FavoriteTeam = FavoriteTeam
            };

            var geolocationService = Xamarin.Forms.DependencyService.Get<IGeolocationService>();

            if(geolocationService != null)
            {
                try
                {
                    var currentLocation = await geolocationService.GetCurrentLocationAsync();
                    newSurvey.Latitude = currentLocation.Item1;
                    newSurvey.Longitude = currentLocation.Item2;
                }catch (Exception)
                {
                    newSurvey.Latitude = 0;
                    newSurvey.Longitude = 0;
                }
            }

            await navigationService.GoBackAsync(new NavigationParameters { { "NewSurvey", newSurvey } });
        }

        private bool EndSurveyCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam);
        }
    }
}
