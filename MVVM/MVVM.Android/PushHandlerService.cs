using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Gcm.Client;
using MVVM;
using MVVM.Droid;
using MVVM.Models;
using MVVM.ViewModels;
using Plugin.Vibrate;
using System;
using System.Collections.Generic;
using System.Text;
using WindowsAzure.Messaging;

namespace Sekiz
{
    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        private NotificationHub Hub { get; set; }
        public static string RegistrationID { get; set; }

        public PushHandlerService() : base(App.SenderID)
        {

        }

        protected override void OnError(Context context, string errorId)
        {
            //Log
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();
            if (intent != null && intent.Extras != null)
            {
                foreach (var item in intent.Extras.KeySet())
                {
                    msg.AppendLine(item + "=" + intent.Extras.Get(item).ToString());
                }
            }

            string SSmessageText;
            SSmessageText = intent.Extras.GetString("sekizdesekiz");
            if (SSmessageText != null)
            {
                CreateNotification("SekizdeSekiz", SSmessageText);
            }

            string ElmessageText;
            ElmessageText = intent.Extras.GetString("elektroy");
            if (ElmessageText != null)
            {
                CreateNotification("Elektroy", ElmessageText);
            }
            string UmmessageText;
            UmmessageText = intent.Extras.GetString("umayana");
            if (UmmessageText != null)
            {
                CreateNotification("UmayAna", UmmessageText);
            }
            string AnonsMessageText;
            AnonsMessageText = intent.Extras.GetString("anons");
            if (AnonsMessageText != null)
            {
                SQLiteManager manager = new SQLiteManager();
                PersonModel _person = new PersonModel();
                string[] anons = AnonsMessageText.Split(',');
                _person.Name = anons[2].ToString();
                _person.Surname = anons[3].ToString();

                CreateNotification("Anons Bildirimi - " + anons[5].ToString(), "Öğrenci: " + anons[2].ToString() + "\nVelisi: " + anons[3].ToString() + " (" + anons[4].ToString() + ")");
                int isInserted = manager.Insert(_person);
                if (isInserted > 0)
                {
                    PersonViewModel per = new PersonViewModel();
                    per.BindData();
                }
            }
        }
        protected override void OnRegistered(Context context, string registrationId)
        {
            RegistrationID = registrationId;

            Hub = new NotificationHub(App.NotificationHubName,
                App.ListenConnectionString, context);

            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch
            {
                //Log
            }

            try
            {
                List<string> tags = new List<string>();
                Hub.Register(registrationId, tags.ToArray());
            }
            catch
            {
                //Log
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            //Log
        }

        void CreateNotification(string title, string desc)
        {

            CrossVibrate.Current.Vibration();

            AssetFileDescriptor asset = Application.Context.Assets.OpenFd("test.mp3");

            PlayFd(asset.FileDescriptor, asset.StartOffset, asset.Length);

            var uiIntent = new Intent(this, typeof(MainActivity));

            const int pendingIntentId = 0;

            PendingIntent pendingIntent = PendingIntent.GetActivity(this, pendingIntentId, uiIntent, PendingIntentFlags.OneShot);

            Notification.Builder builder = new Notification.Builder(this).SetContentIntent(pendingIntent).SetContentTitle(title).SetContentText(desc).SetSmallIcon(MVVM.Droid.Resource.Drawable.satX);

            Notification notification1 = builder.Build();

            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification1);

            //var notification = new Notification(Android.Resource.Drawable.ButtonMinus, title)
            //{
            //    Flags = NotificationFlags.AutoCancel
            //};
            //notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));
            //notificationManager.Notify(1, notification);

            //DialogNotify(title, desc);
        }

        private static void PlayFd(Java.IO.FileDescriptor fd, long offset, long length)
        {
            MediaPlayer player = new MediaPlayer();

            player.Looping = false;
            player.SetDataSource(fd, offset, length);
            player.Prepared += Beep_Hazirla;
            player.PrepareAsync();
            player.Completion += new EventHandler(Beep_Durdur);
        }

        private static void Beep_Hazirla(object sender, EventArgs e)
        {
            MediaPlayer player = (MediaPlayer)sender;

            player.SeekTo(0);
            player.Start();
        }

        private static void Beep_Durdur(object sender, EventArgs e)
        {
            MediaPlayer player = (MediaPlayer)sender;

            player.Completion -= Beep_Durdur;

            player.Stop();
            player.Release();
        }

        void DialogNotify(string title, string desc)
        {
            MainActivity.instance.RunOnUiThread(() =>
            {
                AlertDialog.Builder dlg = new AlertDialog.Builder(MainActivity.instance);
                AlertDialog alert = dlg.Create();
                alert.SetTitle(title);
                alert.SetButton("OK", delegate
                {
                    alert.Dismiss();
                });
                alert.SetMessage(desc);
                alert.Show();
            });
        }
    }
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE },
        Categories = new string[] { App.PackageName })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new string[] { App.PackageName })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new string[] { App.PackageName })]
    public class MyBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public static string[] SENDER_IDS = new string[] { App.SenderID };
        public const string TAG = "MyBroadcastReceiver-GCM";
    }
}