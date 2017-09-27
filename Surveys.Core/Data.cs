using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Surveys.Core
{
    public class Data : NotificationObject
    {
        private ObservableCollection<Survey> surveys;
        private Survey selectedSurvey;
        public ICommand NewSurveyCommand { get; set; }

        public Data()
        {
            Surveys = new ObservableCollection<Survey>();
            MessagingCenter.Subscribe<ContentPage, Survey>(this, Messages.NewSurveyComplete, (sender, args) => 
                {
                    Surveys.Add(args);
                });

            NewSurveyCommand = new Command(NewSurveyCommandExecute);
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
