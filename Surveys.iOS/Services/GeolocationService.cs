using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Surveys.Core.ServiceInterfaces;
using System.Threading.Tasks;
using Surveys.iOS.Services;
using Xamarin.Forms;
using CoreLocation;
using System.Diagnostics;
using Surveys.iOS.Services.Location;
using System.Threading;

[assembly:Dependency(typeof(GeolocationService))]

namespace Surveys.iOS.Services
{
    public class GeolocationService : IGeolocationService
    {
        private static LocationManager manager = null;
        private double latitude;
        private double longitude;
        private bool locationChanged;

        public GeolocationService()
        {
            locationChanged = false;
            latitude = 0;
            longitude = 0;
            manager = new LocationManager();
            manager.LocationUpdated += HandleLocationChanged;
            manager.LocationError += HandleLocationError;
        }

        public async Task<Tuple<double, double>> GetCurrentLocationAsync()
        {
            double lat = -1, lon = -1;
            if(LocationManager.LocationServicesEnabled())
            { 
                manager.StartLocationUpdates();
                while (!locationChanged)
                {
                    await Task.Delay(20).ConfigureAwait(true);
                }
                manager.StopUpdateLocation();
                locationChanged = false;

                lat = latitude;
                lon = longitude;
            }
            return new Tuple<double, double>(lat, lon);
        }

        public void HandleLocationChanged(object sender, LocationUpdatedEventArgs e)
        {
            locationChanged = true;
            latitude = e.Location.Coordinate.Latitude;
            longitude = e.Location.Coordinate.Longitude;
        }

        public void HandleLocationError(object sender, NSErrorEventArgs e)
        {
            locationChanged = true;
        }
    }
}