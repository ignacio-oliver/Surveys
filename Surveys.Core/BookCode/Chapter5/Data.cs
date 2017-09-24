using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Core.BookCode.Chapter5
{
    public class Data : Notifiable
    {
        public Data()
        {
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

        private ObservableCollection<Person> persons;
        private Person selectedPerson;
        
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
    }
}
