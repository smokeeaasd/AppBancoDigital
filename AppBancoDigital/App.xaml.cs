using AppBancoDigital.Models;
using AppBancoDigital.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace AppBancoDigital
{
    public partial class App : Application
    {
        public event EventHandler OnTransacaoRecebida;
        public App()
        {
			Device.SetFlags(new string[] { "AppTheme_Experimental" });
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            InitializeComponent();
			if (App.Current.Properties.ContainsKey("id_correntista"))
			{
                int id_correntista = int.Parse(App.Current.Properties["id_correntista"].ToString());

            	MainPage = new NavigationPage(new Views.Home()
				{
					BindingContext = App.Current.Properties["id_correntista"] as int?
				});
			} else {
				MainPage = new NavigationPage(new Views.Inicial());
			}
        }

        private async Task<List<Conta>> GetContas(int id_correntista)
        {
            List<Conta> contas = await DataServiceConta.GetContasByCorrentista(id_correntista);

            return contas;
        }

        private async void LogTransacoes(int id_correntista)
        {
            List<Conta> contas = await GetContas(id_correntista);


            foreach (Conta c in contas)
            {
                Transacao tc = await DataServiceTransacao.GetUltimaByDestinatario(c.Id);

                AppServices.SendLogRequest("BancoDigital", JsonConvert.SerializeObject(tc));
                AppServices.SendLogRequest("BancoDigital", JsonConvert.SerializeObject(tc));

                await Task.Delay(1000);
            }

            LogTransacoes(id_correntista);
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
