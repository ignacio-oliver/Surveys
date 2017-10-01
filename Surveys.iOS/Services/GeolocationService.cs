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

[assembly:Dependency(typeof(GeolocationService))]

namespace Surveys.iOS.Services
{
    public class GeolocationService : IGeolocationService
    {
        public Task<Tuple<double, double>> GetCurrentLocationAsync()
        {
            /* ToDo : implementar el acceso a la ubicaciòn iOS */
            var result = new Tuple<double, double>(0, 0);
            return Task.FromResult(result);
        }
    }
}