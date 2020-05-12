using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AppCenter.Analytics;

namespace CovidCorpus.Droid.Callbacks
{
    public class MyAdvertiseCallback : AdvertiseCallback
    {
        MainActivity mainActivity;

        public MyAdvertiseCallback()
        {
            
        }

        public override void OnStartSuccess(AdvertiseSettings settingsInEffect)
        {
            base.OnStartSuccess(settingsInEffect);

            Analytics.TrackEvent(Build.Model + " Advertise started.");
            //mainActivity.isAdvertising = true;

            //mainActivity.RunOnUiThread(() =>
            //{
            //    mainActivity.logTextView.Text = mainActivity.logTextView.Text + "Advertise started successfully";
            //});
        }

        public override void OnStartFailure([GeneratedEnum] AdvertiseFailure errorCode)
        {
            base.OnStartFailure(errorCode);

            Analytics.TrackEvent(Build.Model + " Advertise failed.");
            //mainActivity.isAdvertising = true;

            //mainActivity.RunOnUiThread(() =>
            //{
            //    mainActivity.logTextView.Text = mainActivity.logTextView.Text + "Advertise failed";
            //});
            //Log.e("BLE", "Advertising onStartFailure: " + errorCode);
        }
    }
}