using Surveys.Core.Models;
using Surveys.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Surveys.Core.ViewModels
{
    public class SurveyDetailsViewModel : NotificationObject
    {
        private string name;
        private DateTime birthdate;
        private string favoriteTeam;
        private ObservableCollection<string> teams;

        public ICommand SelectTeamCommand { get; set; }
        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel()
        {
            Teams = new ObservableCollection<string>(new[] 
            {
                "Alianza Lima",
                "América",
                "Boca Juniors",
                "Caracas FC",
                "Colo-Colo",
                "Nacional",
                "Real Madrid",
                "Saprissa"
            });

            SelectTeamCommand = new Command(SelectTeamCommandExecute);
            EndSurveyCommand = new Command(EndSurveyCommandExecute, EndSurveyCommandCanExecute);

            PropertyChanged += SurveyDetailsViewModel_PropertyChanged;

            MessagingCenter.Subscribe<ContentPage, string>(this, Messages.TeamSelected, (sender, args) => { FavoriteTeam = args; });
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(name == value)
                {
                    return;
                }
                name = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthdate
        {
            get
            {
                return birthdate;
            }
            set
            {
                if(birthdate == value)
                {
                    return;
                }
                birthdate = value;
                OnPropertyChanged();
            }
        }

        public string FavoriteTeam
        {
            get
            {
                return favoriteTeam;
            }
            set
            {
                if(favoriteTeam == value)
                {
                    return;
                }
                favoriteTeam = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                if(teams == value)
                {
                    return;
                }
                teams = value;
                OnPropertyChanged();
            }
        }

        private void SelectTeamCommandExecute()
        {
            MessagingCenter.Send(this, Messages.SelectTeam, Teams.ToArray());
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Name = Name,
                Birthdate = Birthdate,
                FavoriteTeam = FavoriteTeam
            };

            var geolocationService = DependencyService.Get<IGeolocationService>();

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

            MessagingCenter.Send(this, Messages.NewSurveyComplete, newSurvey);

        }

        private bool EndSurveyCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam);
        }

        private void SurveyDetailsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(FavoriteTeam))
            {
                (EndSurveyCommand as Command)?.ChangeCanExecute();
            }
        }
    }
}
