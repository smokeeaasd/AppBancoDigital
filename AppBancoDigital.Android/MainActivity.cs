using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.App;
using Android.Widget;
using AndroidX.AppCompat;
using AndroidX.AppCompat.App;
using Android.Util;
using Xamarin.Forms;
using AppBancoDigital;

namespace AppBancoDigital.Droid
{
	[Activity(Label = "AppBancoDigital", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			AppServices.OnLogRequest += LogRecebido;
			AppServices.OnNotificationRequest += NotificacaoRecebida;

			AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
			Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#000000"));

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			LoadApplication(new App());
		}

		private void LogRecebido(object sender, EventArgs e)
		{
			LogData data = sender as LogData;
			Log.Debug(data.Tag, data.Message);
		}

		private void NotificacaoRecebida(object sender, EventArgs e)
		{
			try
			{
				NotificationData data = sender as NotificationData;

				NotificationChannel channel = new NotificationChannel(data.ChannelId, data.ChannelName, NotificationImportance.Default);
				channel.Description = data.Description;
				channel.Importance = NotificationImportance.Default;

				var nmr = GetSystemService(Context.NotificationService) as NotificationManager;
				nmr.CreateNotificationChannel(channel);

				var builder = new NotificationCompat.Builder(this, data.ChannelId);

				builder.SetContentTitle(data.Title)
					.SetContentText(data.Message)
					.SetSmallIcon(Resource.Mipmap.icon);

				Notification notification = builder.Build();

				nmr.Notify(data.NotificationId, notification);
			}
			catch (Exception ex)
			{
				AppServices.SendLogRequest("AppBanco", ex.Message);
				AppServices.SendLogRequest("AppBanco", ex.StackTrace);
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}