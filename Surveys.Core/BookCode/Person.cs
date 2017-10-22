using System;

namespace Surveys.Core.BookCode
{
    public class Person : Notifiable
    {
        private string name;
        private string country;
        private DateTime birthDate;
        private decimal balance;

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
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                if(country == value)
                {
                    return;
                }
                country = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if(birthDate == value)
                {
                    return;
                }
                birthDate = value;
                OnPropertyChanged();
            }
        }

        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if(balance == value)
                {
                    return;
                }
                balance = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"{Name}|{Country}|{BirthDate}|{Balance}";
        }
    }
}
