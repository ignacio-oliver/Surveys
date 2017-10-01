using Surveys.Core.ServiceInterfaces;
using Surveys.UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Windows.Devices.Geolocation;

[assembly:Dependency(typeof(GeolocationService))]

namespace Surveys.UWP.Services
{
    public class GeolocationService : IGeolocationService
    {
        public async Task<Tuple<double, double>> GetCurrentLocationAsync()
        {
            var geolocator = new Geolocator();
            var position = await geolocator.GetGeopositionAsync();
            var result = new Tuple<double, double>(position.Coordinate.Point.Position.Latitude, position.Coordinate.Point.Position.Longitude);
            return result;
        }
    }
}
