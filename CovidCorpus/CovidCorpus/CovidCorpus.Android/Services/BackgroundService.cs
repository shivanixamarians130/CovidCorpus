using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace CovidCorpus.Droid.Services
{
    [Service(Exported = true)]
    class BackgroundService : Service
    {
        private Timer timer;
        private RunningTasks runningTasks;
        public int counter = 0;
        ISharedPreferences prefs;

        public override void OnCreate()
        {
            base.OnCreate();
            //SplashActivity.BackgroundEvent += Activity_BackgroundEvent;
            //LoginActivity.BackgroundEvent += Activity_BackgroundEvent;
            //ForgotPasswordActivity.BackgroundEvent += Activity_BackgroundEvent;
            //MenuActivity.BackgroundEvent += Activity_BackgroundEvent;
            //ReportsActivity.BackgroundEvent += Activity_BackgroundEvent;
            //SerialCodeActivity.BackgroundEvent += Activity_BackgroundEvent;
            //SerialCodeDetailsActivity.BackgroundEvent += Activity_BackgroundEvent;
            //SettingsActivity.BackgroundEvent += Activity_BackgroundEvent;
            //prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var notificationChannel = new NotificationChannel("default", "Background Service", NotificationImportance.Low);
                notificationChannel.Description = "Scanning....";

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(notificationChannel);

                NotificationCompat.Builder builder = new NotificationCompat.Builder(this, "default")
                    .SetContentTitle("Covid Corpus")
                    .SetContentText("Scanning...");
                   // .SetSmallIcon(Resource.Drawable.ic_icon);
                Notification notification = builder.Build();
                StartForeground(1, notification);
            }
        }

        private void Activity_BackgroundEvent(object sender, EventArgs e)
        {
            OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            StartTimer();
            return StartCommandResult.Sticky;
        }

        public void StartTimer()
        {
            timer = new Timer(onTimerCallBack, null, 0, 1000);
        }

        private void onTimerCallBack(object state)
        {
            runningTasks = new RunningTasks();
            runningTasks.Run();
        }

        public override void OnTaskRemoved(Intent rootIntent)
        {
            base.OnTaskRemoved(rootIntent);
            OnDestroy();
        }
        /// <summary>
        /// Called whenever app is closed.
        /// </summary>
        public override void OnDestroy()
        {
            try
            {
                Intent ll24 = new Intent(this, typeof(BackgroundServiceRestarterBroadcast));
                    SendBroadcast(ll24);
                Stoptimertask();
                base.OnDestroy();
            }
            catch (Exception ex)
            {
            }
        }
        public void Stoptimertask()
        {
            try
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            catch (Exception ex)
            {
            }
        }
    }
}