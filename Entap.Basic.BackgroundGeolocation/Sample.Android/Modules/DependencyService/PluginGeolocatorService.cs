using System;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

[assembly: Dependency(typeof(LRMS.Droid.PluginGeolocatorService))]
namespace LRMS.Droid
{
    public class PluginGeolocatorService : IPluginGeolocatorService
    {
        public IGeolocator GetGeolocator()
        {
            throw new NotImplementedException();
        }
    }
}
