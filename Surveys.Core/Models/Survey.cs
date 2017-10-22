using System;

namespace Surveys.Core.Models
{
    public class Survey
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string FavoriteTeam { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Name} | {Birthdate} | {FavoriteTeam} | {Latitude} | {Longitude}";
        }
    }
}
