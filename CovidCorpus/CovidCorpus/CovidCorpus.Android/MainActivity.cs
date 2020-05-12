using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using CovidCorpus.Droid.Services;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using System.Text;
using CovidCorpus.Droid.Callbacks;
using Java.Util;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;

namespace CovidCorpus.Droid
{
    [Activity(Label = "CovidCorpus", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static List<DetectedUserInfoModel> DetectedList = new List<DetectedUserInfoModel>();

        Intent mServiceIntent;
        public const string UUIDString = "0000b81d-0000-1000-8000-00805f9b34fb";
        public static UUID MY_UUID = UUID.FromString(UUIDString);

        public MyScanCallback myScanCallback;
        public MyAdvertiseCallback myAdvertiseCallback;

        public BluetoothLeAdvertiser bluetoothLeAdvertiser;
        public BluetoothLeScanner bluetoothLeScanner;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var pixels = Resources.DisplayMetrics.WidthPixels;
            var scale = Resources.DisplayMetrics.Density;
            var dps = (double)((pixels - 0.5f) / scale);
            var ScreenWidth = (int)dps;
            App.ScreenWidth = ScreenWidth;
            pixels = Resources.DisplayMetrics.HeightPixels;
            dps = (double)((pixels - 0.5f) / scale);
            var ScreenHeight = (int)dps;
            App.ScreenHeight = ScreenHeight;


            LoadApplication(new App());

            AppCenter.Start("0e29a028-9dfa-4b07-9013-21dcf4ea7761",
                   typeof(Analytics), typeof(Crashes));

            //Advertise();
            //Discover();
           
        }

        //public void Advertise()
        //{
        //    try
        //    {
        //        Analytics.TrackEvent(Build.Model + " Advertise method called.");

        //        bluetoothLeAdvertiser = BluetoothAdapter.DefaultAdapter.BluetoothLeAdvertiser;

        //        AdvertiseSettings settings = new AdvertiseSettings.Builder()
        //                .SetAdvertiseMode(AdvertiseMode.LowLatency)
        //                .SetTxPowerLevel(AdvertiseTx.PowerHigh)
        //                .SetTimeout(0)
        //                .SetConnectable(true)
        //                .Build();

        //        ParcelUuid pUuid = new ParcelUuid(MY_UUID);

        //        AdvertiseData data = new AdvertiseData.Builder()
        //                .SetIncludeDeviceName(true)
        //                .AddServiceUuid(pUuid)
        //                .AddServiceData(pUuid, Encoding.UTF8.GetBytes("222"))
        //                .Build();

        //        myAdvertiseCallback = new MyAdvertiseCallback();

        //        bluetoothLeAdvertiser.StartAdvertising(settings, data, myAdvertiseCallback);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Analytics.TrackEvent(Build.Model + " Something went wrong in Advertise method.");
        //    }
        //}

        //public void Discover()
        //{
        //    try
        //    {
        //        Analytics.TrackEvent(Build.Model + " Discover method called.");

        //        List<ScanFilter> filters = new List<ScanFilter>();

        //        ScanFilter filter = new ScanFilter.Builder()
        //                .SetServiceUuid(new ParcelUuid(MY_UUID))
        //                .Build();
        //        filters.Add(filter);

        //        //ScanSettings settings = new ScanSettings.Builder()
        //        //        .SetScanMode(Android.Bluetooth.LE.ScanMode.LowLatency)
        //        //        .Build();

        //        ScanSettings.Builder builder = new ScanSettings.Builder();
        //        builder.SetScanMode(Android.Bluetooth.LE.ScanMode.LowLatency);

        //        if (Build.VERSION.SdkInt >= BuildVersionCodes.M /* Marshmallow */)
        //        {
        //            builder.SetMatchMode(BluetoothScanMatchMode.Aggressive);
        //            builder.SetNumOfMatches((int)BluetoothScanMatchNumber.MaxAdvertisement);
        //            builder.SetCallbackType(ScanCallbackType.AllMatches);
        //        }

        //        var settings = builder.Build();

        //        myScanCallback = new MyScanCallback(this);
        //        bluetoothLeScanner = BluetoothAdapter.DefaultAdapter.BluetoothLeScanner;


        //        bluetoothLeScanner.StartScan(filters, settings, myScanCallback);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Analytics.TrackEvent(Build.Model + " Something went wrong in Discover method.");
        //    }
        //}




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private bool IsMyServiceRunning(Java.Lang.Class serviceClass)
        {
            ActivityManager manager = (ActivityManager)GetSystemService(Context.ActivityService);
            foreach (var item in manager.GetRunningServices(int.MaxValue))
            {
                if (serviceClass.Name.Equals(item.Service.ClassName))
                {
                    return true;
                }
            }
            return false;
        }

    }

    public class DetectedUserInfoModel
    {
        public string UserId { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}