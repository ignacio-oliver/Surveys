using System;
using System.Threading.Tasks;
using Android.Content;
using Surveys.Core.ServiceInterfaces;
using Surveys.Droid.Services;
using Xamarin.Forms;
using Android.Locations;

[assembly:Dependency(typeof(GeolocationService))]

namespace Surveys.Droid.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly LocationManager locationManager = null;

        public GeolocationService()
        {
            locationManager = Xamarin.Forms.Forms.Context.GetSystemService(Context.LocationService) as LocationManager;
        }

        public Task<Tuple<double, double>> GetCurrentLocationAsync()
        {
            var location = GetLastKnownLocation();
            var result = new Tuple<double, double>(location.Latitude, location.Longitude);
            return Task.FromResult(result);
        }

        private Location GetLastKnownLocation()
        {
    
            System.Collections.Generic.IList<String> providers = locationManager.GetProviders(true);
            Location bestLocation = null;
            foreach (String provider in providers)
            {
                Location l = locationManager.GetLastKnownLocation(provider);
                if (l == null)
                {
                    continue;
                }
                if (bestLocation == null || l.Accuracy < bestLocation.Accuracy)
                {
                    // Found best last known location: %s", l);
                    bestLocation = l;
                }
            }
            return bestLocation;
        }
    }
}