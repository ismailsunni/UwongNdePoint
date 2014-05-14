using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;

namespace UwongNdePoint
{
  [Activity(Label = "UwongNdePoint", MainLauncher = true)]
  public class MainActivity : Activity
  {
    int count = 1;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.Main);

      MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
      GoogleMap map = mapFragment.Map;
      // TODO: move this to a listener instead
      map.MyLocationEnabled = true;

      var location = map.MyLocation;
      if (location == null)
      {
        LocationManager locationManager = (LocationManager) GetSystemService(Service.LocationService);
        Criteria criteria = new Criteria();
        String provider = locationManager.GetBestProvider(criteria, true);
        location = locationManager.GetLastKnownLocation(provider);
      }
      if (location != null)
      {
        // TODO: get marker from Parse
        var latlong = new LatLng(location.Latitude, location.Longitude);
        map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(latlong, 16));

        MarkerOptions markerOption = new MarkerOptions();
        markerOption.SetPosition(latlong);
        markerOption.SetTitle("Ganteng");
        markerOption.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Icon));
        map.AddMarker(markerOption);
      }
    }
  }
}


