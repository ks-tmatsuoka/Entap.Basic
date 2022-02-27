using System;
using Plugin.Geolocator.Abstractions;

namespace LRMS
{
    public interface IPluginGeolocatorService
    {
        IGeolocator GetGeolocator();
    }
}
