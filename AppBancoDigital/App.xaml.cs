using AppBancoDigital.Services;
using System;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace AppBancoDigital
{
    public partial class App : Application
    {
        public App()
        {
			Device.SetFlags(new string[] { "AppTheme_Experimental" });
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            InitializeComponent();
			if (App.Current.Properties.ContainsKey("id_correntista"))
			{
            	MainPage = new NavigationPage(new Views.Home()
				{
					BindingContext = App.Current.Properties["id_correntista"] as int?
				});
			} else {
				MainPage = new NavigationPage(new Views.Inicial());
			}
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
