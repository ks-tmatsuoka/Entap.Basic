using System;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

[assembly: Dependency(typeof(LRMS.iOS.PluginGeolocatorService))]
namespace LRMS.iOS
{
    public class PluginGeolocatorService : IPluginGeolocatorService
    {
        public IGeolocator GetGeolocator() => new GeolocatorImplementation();
    }
}
