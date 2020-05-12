using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CovidCorpus.Droid.Services
{
    [BroadcastReceiver(Enabled = true)]
    class BackgroundServiceRestarterBroadcast : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                context.StartForegroundService(new Intent(context, typeof(BackgroundService)));
            else
                context.StartService(new Intent(context, typeof(BackgroundService)));
        }
    }
}