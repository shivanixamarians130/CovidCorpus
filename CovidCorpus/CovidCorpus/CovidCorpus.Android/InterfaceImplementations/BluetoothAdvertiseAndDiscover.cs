

using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.OS;
using CovidCorpus.Droid.Callbacks;
using CovidCorpus.Droid.InterfaceImplementations;
using CovidCorpus.Interfaces;
using Java.Util;
using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(BluetoothAdvertiseAndDiscover))]

namespace CovidCorpus.Droid.InterfaceImplementations
{

    class BluetoothAdvertiseAndDiscover : IAdvertiseAndDiscoverBluetoothDevice
    {

        public const string UUIDString = "0000b81d-0000-1000-8000-00805f9b34fb";
        public static UUID MY_UUID = UUID.FromString(UUIDString);

        public MyScanCallback myScanCallback;
        public MyAdvertiseCallback myAdvertiseCallback;

        public BluetoothLeAdvertiser bluetoothLeAdvertiser;
        public BluetoothLeScanner bluetoothLeScanner;

        void IAdvertiseAndDiscoverBluetoothDevice.Advertise()
        {
            try
            {
                Analytics.TrackEvent(Build.Model + " Advertise method called.");

                bluetoothLeAdvertiser = BluetoothAdapter.DefaultAdapter.BluetoothLeAdvertiser;

                AdvertiseSettings settings = new AdvertiseSettings.Builder()
                        .SetAdvertiseMode(AdvertiseMode.LowLatency)
                        .SetTxPowerLevel(AdvertiseTx.PowerHigh)
                        .SetTimeout(0)
                        .SetConnectable(true)
                        .Build();

                ParcelUuid pUuid = new ParcelUuid(MY_UUID);

                AdvertiseData data = new AdvertiseData.Builder()
                        .SetIncludeDeviceName(true)
                        .AddServiceUuid(pUuid)
                        .AddServiceData(pUuid, Encoding.UTF8.GetBytes("222"))
                        .Build();

                myAdvertiseCallback = new MyAdvertiseCallback();

                bluetoothLeAdvertiser.StartAdvertising(settings, data, myAdvertiseCallback);
            }
            catch (System.Exception ex)
            {
                Analytics.TrackEvent(Build.Model + " Something went wrong in Advertise method.");
            }
        }
        

        void IAdvertiseAndDiscoverBluetoothDevice.Discover()
        {
            try
            {
                Analytics.TrackEvent(Build.Model + " Discover method called.");

                List<ScanFilter> filters = new List<ScanFilter>();

                ScanFilter filter = new ScanFilter.Builder()
                        .SetServiceUuid(new ParcelUuid(MY_UUID))
                        .Build();
                filters.Add(filter);

                //ScanSettings settings = new ScanSettings.Builder()
                //        .SetScanMode(Android.Bluetooth.LE.ScanMode.LowLatency)
                //        .Build();

                ScanSettings.Builder builder = new ScanSettings.Builder();
                builder.SetScanMode(Android.Bluetooth.LE.ScanMode.LowLatency);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.M /* Marshmallow */)
                {
                    builder.SetMatchMode(BluetoothScanMatchMode.Aggressive);
                    builder.SetNumOfMatches((int)BluetoothScanMatchNumber.MaxAdvertisement);
                    builder.SetCallbackType(ScanCallbackType.AllMatches);
                }

                var settings = builder.Build();

                myScanCallback = new MyScanCallback();
                bluetoothLeScanner = BluetoothAdapter.DefaultAdapter.BluetoothLeScanner;


                bluetoothLeScanner.StartScan(filters, settings, myScanCallback);
            }
            catch (System.Exception ex)
            {
                Analytics.TrackEvent(Build.Model + " Something went wrong in Discover method.");
            }
        }
    }
}