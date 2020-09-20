//using Android.App;
//using Android.Content;
//using Android.Gms.Common.Apis;
//using Android.Gms.Location;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace SimTuning.Droid
//{
//    public static class Functions
//    {
//        public const int REQUEST_CHECK_SETTINGS = 0x1;

// public static void DisplayLocationSettingsRequest() { var googleApiClient = new
// GoogleApiClient.Builder(this).AddApi(LocationServices.API).Build();
// googleApiClient.Connect();

// var locationRequest = LocationRequest.Create();
// locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
// locationRequest.SetInterval(10000); locationRequest.SetFastestInterval(10000 / 2);

// var builder = new
// LocationSettingsRequest.Builder().AddLocationRequest(locationRequest);
// builder.SetAlwaysShow(true);

// var result = LocationServices.SettingsApi.CheckLocationSettings(googleApiClient,
// builder.Build()); result.SetResultCallback((LocationSettingsResult callback) => {
// switch (callback.Status.StatusCode) { case LocationSettingsStatusCodes.Success: {
// //DoStuffWithLocation(); break; } case LocationSettingsStatusCodes.ResolutionRequired:
// { try { // Show the dialog by calling startResolutionForResult(), and check the result
// // in onActivityResult(). //callback.Status.StartResolutionForResult(this,
// REQUEST_CHECK_SETTINGS); } catch (IntentSender.SendIntentException e) { }

// break; } default: { // If all else fails, take the user to the android location
// settings //StartActivity(new
// Intent(Android.Provider.Settings.ActionLocationSourceSettings)); break; } } }); }

//        protected static void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
//        {
//            switch (requestCode)
//            {
//                case REQUEST_CHECK_SETTINGS:
//                    {
//                        switch (resultCode)
//                        {
//                            case Android.App.Result.Ok:
//                                {
//                                    //DoStuffWithLocation();
//                                    break;
//                                }
//                            case Android.App.Result.Canceled:
//                                {
//                                    //No location
//                                    break;
//                                }
//                        }
//                        break;
//                    }
//            }
//        }
//    }
//}