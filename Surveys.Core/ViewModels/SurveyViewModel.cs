using Surveys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Core.ViewModels
{
    public class SurveyViewModel : ViewModelBase
    {
        #region Properties
        public string Id { get; set; }
        private string name;
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
                if(birthdate == value)
                {
                    return;
                }
                birthdate = value;
                RaisePropertyChanged();
            }
        }
        private TeamViewModel team;
        public TeamViewModel Team
        {
            get
            {
                return team;
            }
            set
            {
                if(team == value)
                {
                    return;
                }
                team = value;
                RaisePropertyChanged();
            }
        }
        private double latitude;
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                if(latitude == value)
                {
                    return;
                }
                latitude = value;
                RaisePropertyChanged();
            }
        }
        private double longitude;
        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                if(longitude == value)
                {
                    return;
                }
                longitude = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public static SurveyViewModel GetViewModelFromEntity(Survey entity, IEnumerable<Team> teams)
        {
            var result = new SurveyViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Birthdate = entity.Birthdate,
                Team = TeamViewModel.GetViewModelFromEntity(teams.First(t => t.Id == entity.TeamId)),
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };
            return result;
        }

        public static Survey GetEntityFromViewModel(SurveyViewModel viewModel)
        {
            var result = new Survey
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Birthdate = viewModel.Birthdate,
                TeamId = viewModel.Team.Id,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude
            };
            return result;
        }
    }
}
