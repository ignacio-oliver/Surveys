using Surveys.Core.Models;
using Surveys.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Surveys.Core.ViewModels
{
    public class SurveysViewModel : NotificationObject
    {
        private ObservableCollection<Survey> surveys;
        private Survey selectedSurvey;
        public ICommand NewSurveyCommand { get; set; }

        public SurveysViewModel()
        {
            Surveys = new ObservableCollection<Survey>();

            NewSurveyCommand = new Command(NewSurveyCommandExecute);

            MessagingCenter.Subscribe<SurveyDetailsViewModel, Survey>(this, Messages.NewSurveyComplete, (sender, args) => 
                {
                    Surveys.Add(args);
                });
        }
        public ObservableCollection<Survey> Surveys
        {
            get
            {
                return surveys;
            }
            set
            {
                if(surveys == value)
                {
                    return;
                }
                surveys = value;
                OnPropertyChanged();
            }
        }

        public Survey SelectedSurvey
        {
            get
            {
                return selectedSurvey;
            }
            set
            {
                if(selectedSurvey == value)
                {
                    return;
                }
                selectedSurvey = value;
                OnPropertyChanged();
            }
        }

        private void NewSurveyCommandExecute()
        {
            MessagingCenter.Send(this, Messages.NewSurvey);
        }
    }
}
