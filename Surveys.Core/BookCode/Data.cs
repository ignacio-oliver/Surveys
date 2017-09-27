using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Surveys.Core.BookCode
{
    public class Data : Notifiable
    {
        private ObservableCollection<Person> persons;
        private Person selectedPerson;

        public ICommand AddPersonCommand { get; set; }

        public Data()
        {
            //AddPersonCommand = new MyCommand(AddPersonCommandExecute, AddPersonCommandCanExecute);
            AddPersonCommand = new Command(AddPersonCommandExecute, AddPersonCommandCanExecute);

            Persons = new ObservableCollection<Person>();
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                Persons.Add(new Person()
                    {
                        Name = $"Person {i}",
                        Country = $"Country {i}",
                        BirthDate = new DateTime(1980 + i, i + 1, 1),
                        Balance = (decimal)(random.Next(100, 5000) * 3.1416)
                    }
                );
            }
        }
        
        public ObservableCollection<Person> Persons
        {
            get
            {
                return persons;
            }
            set
            {
                if (persons == value)
                {
                    return;
                }
                persons = value;
                OnPropertyChanged();
            }
        }

        public Person SelectedPerson
        {
            get
            {
                return selectedPerson;
            }
            set
            {
                if(selectedPerson == value)
                {
                    return;
                }
                selectedPerson = value;
                OnPropertyChanged();
            }

        }

        private void AddPersonCommandExecute()
        {
            Persons.Add(new Person()
            {
                Name = Guid.NewGuid().ToString(),
                Country = Guid.NewGuid().ToString(),
                BirthDate = new DateTime(1980, 10, 10),
                Balance = 0
            });
            //(AddPersonCommand as MyCommand)?.RaiseCanExecuteChanged();
            (AddPersonCommand as Command)?.ChangeCanExecute();
        }

        private bool AddPersonCommandCanExecute()
        {
            return Persons.Count < 10;
        }
    }
}
