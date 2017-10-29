using System;

namespace Surveys.Core.Models
{
    public class Survey
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string FavoriteTeam { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Birthdate} | {FavoriteTeam} | {Latitude} | {Longitude}";
        }
    }
}
