using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreLocation;
using System.Diagnostics;

namespace Surveys.iOS.Services.Location
{
    public class LocationManager
    {
        protected CLLocationManager locMgr;
        public event EventHandler<LocationUpdatedEventArgs> LocationUpdated = delegate { };
        public event EventHandler<NSErrorEventArgs> LocationError = delegate { };

        public LocationManager()
        {
            this.locMgr = new CLLocationManager();
            this.locMgr.PausesLocationUpdatesAutomatically = false;
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                locMgr.RequestAlwaysAuthorization();
                //locMgr.RequestWhenInUseAuthorization ();
            }
            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                locMgr.AllowsBackgroundLocationUpdates = true;
            }
        }

        public static bool LocationServicesEnabled()
        {
            return CLLocationManager.LocationServicesEnabled;
        }

        public CLLocationManager LocMgr
        {
            get { return this.locMgr; }
        }

        public void StartLocationUpdates()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                LocMgr.DesiredAccuracy = 1; 
                if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
                {
                    LocMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) => {
                        this.LocationUpdated(this, new LocationUpdatedEventArgs(e.Locations[e.Locations.Length - 1]));
                    };
                }
                else
                {
                    LocMgr.UpdatedLocation += (object sender, CLLocationUpdatedEventArgs e) => {
                        this.LocationUpdated(this, new LocationUpdatedEventArgs(e.NewLocation));
                    };
                }
                LocMgr.StartUpdatingLocation();
                LocMgr.Failed += (object sender, NSErrorEventArgs e) => {
                    this.LocationError(this, e);
                };
            }
        }

        public void StopUpdateLocation()
        {
            locMgr.StopUpdatingLocation();
        }
    }
}