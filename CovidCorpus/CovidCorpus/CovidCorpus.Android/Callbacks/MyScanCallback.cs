using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Microsoft.AppCenter.Analytics;

namespace CovidCorpus.Droid.Callbacks
{
   public class MyScanCallback : ScanCallback
    {

        const string NotificationChannelId = "CovidPositiveDetected";
        const int NOTIFICATIONID = 1;
        int count = 0;
        public MyScanCallback()
        {
        }

        public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
        {
            base.OnScanResult(callbackType, result);

            //mainActivity.RunOnUiThread(() =>
            //{
            //  //  mainActivity.logTextView.Text = mainActivity.logTextView.Text + "Discover result called";
            //});

            if (result == null || result.Device == null || TextUtils.IsEmpty(result.Device.Name))
                return;

            if (count == 0)
            {
                Analytics.TrackEvent(Build.Model + " New device " + result.Device.Name + " detected.");
                count++;
            }

            //StringBuilder builder = new StringBuilder(result.Device.Name);
            //builder.Append(" ").Append(result.Device.Address);
            byte[] data;
            result.ScanRecord.ServiceData.TryGetValue(result.ScanRecord.ServiceUuids[0], out data);
            var remoteDeviceUserId = Encoding.UTF8.GetString(data);

            var temp = App.UserInfoList.Where(s => s.UserId == remoteDeviceUserId).FirstOrDefault();
            if (temp != null)
            {
                if (temp.Status == "Positive")
                {
                    var r = (MainActivity.DetectedList.Where(s => s.UserId == remoteDeviceUserId)).FirstOrDefault();

                    if (r != null)
                    {
                        if ((DateTime.Now - r.DateAndTime).TotalMinutes >= 30)
                        {
                            //Update date time of id.

                            //Show local notification
                            ShowNotification();

                        }
                    }
                    else
                    {
                        Analytics.TrackEvent(Build.Model + " New device detected and Notification sent.");
                        MainActivity.DetectedList.Add(new DetectedUserInfoModel { UserId = temp.UserId, DateAndTime = DateTime.Now });
                        //Show notification
                        ShowNotification();
                    }
                }
            }

            //mainActivity.success++;
            //  mainActivity.mSuccessCountText.Text = mainActivity.success.ToString();
            //mainActivity.mText.Text = mainActivity.mText.Text + builder.ToString();


            //mainActivity.RunOnUiThread(() =>
            //{
            //  //  mainActivity.logTextView.Text = mainActivity.logTextView.Text + "Connect gatt";
            //});

            //mainActivity.bluetoothGatt = null;
            //mainActivity.bluetoothGatt = result.Device.ConnectGatt(mainActivity, false, mainActivity.myBluetoothGattCallback);

            //mainActivity.bluetoothLeScanner.StopScan(mainActivity.myScanCallback);

            //if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            //{
            //    mainActivity.RunOnUiThread(() =>
            //    {
            //        mainActivity.logTextView.Text = mainActivity.logTextView.Text + "Connect gatt Marshmallow a";
            //    });
            //    mainActivity.bluetoothGatt = result.Device.ConnectGatt(mainActivity, false, mainActivity.myBluetoothGattCallback);
            //}
            //else
            //{
            //    mainActivity.RunOnUiThread(() =>
            //    {
            //        mainActivity.logTextView.Text = mainActivity.logTextView.Text + "Connect gatt marshmallow previous";
            //    });
            //    mainActivity.bluetoothGatt = result.Device.ConnectGatt(mainActivity, false, mainActivity.myBluetoothGattCallback);
            //}
        }

        public override void OnBatchScanResults(IList<ScanResult> results)
        {
            base.OnBatchScanResults(results);
        }

        public override void OnScanFailed([GeneratedEnum] ScanFailure errorCode)
        {
            base.OnScanFailed(errorCode);

            Analytics.TrackEvent(Build.Model + " Scan failed.");
            //mainActivity.failed++;
            //mainActivity.mFailedCountText.Text = mainActivity.failed.ToString();
            //mainActivity.RunOnUiThread(() =>
            //{
            //    mainActivity.logTextView.Text = mainActivity.logTextView.Text + "Discover failed" + errorCode.ToString();
            //});
        }

        private void CreateNotificationChannel()
        {
            // Create the NotificationChannel, but only on API 26+ because
            // the NotificationChannel class is new and not in the support library
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                string name = "ChannelName";
                string description = "ChannelDescription";
                NotificationChannel channel = new NotificationChannel(NotificationChannelId, name, NotificationImportance.Default);
                channel.Description = description;
                // Register the channel with the system; you can't change the importance
                // or other notification behaviors after this
                NotificationManager notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }
        }

        private void ShowNotification()
        {
            // Create the PendingIntent with the back stack:
            //var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            CreateNotificationChannel();

            // Build the notification:
            var builder = new NotificationCompat.Builder(Application.Context, NotificationChannelId)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                                               //.SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle("Covid Corpus") // Set the title
                                                           //.SetNumber(count) // Display the count in the Content Info
                          .SetSmallIcon(Resource.Drawable.ic_logo) // This is the icon to display
                          .SetContentText("Someone near you is Covid 19 positive."); // the message to display.

            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(NOTIFICATIONID, builder.Build());
        }
    }
}