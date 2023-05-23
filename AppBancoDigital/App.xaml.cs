using AppBancoDigital.Models;
using AppBancoDigital.Services;
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
        public App()
        {
			Device.SetFlags(new string[] { "AppTheme_Experimental" });
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            InitializeComponent();
			if (App.Current.Properties.ContainsKey("id_correntista"))
			{
                int idCorrentista = (int)App.Current.Properties["id_correntista"];
                try
                {
                    ListenForTransacao(idCorrentista);
                }
                catch (Exception ex)
                {
                    AppServices.SendLogRequest("AppBanco", ex.Message);
                    AppServices.SendLogRequest("AppBanco", ex.StackTrace);
                }

            	MainPage = new NavigationPage(new Views.Home()
				{
					BindingContext = App.Current.Properties["id_correntista"] as int?
				});
			} else {
				MainPage = new NavigationPage(new Views.Inicial());
			}
        }

        private async Task<List<Conta>> GetContas(int id_conta)
        {
            List<Conta> contas = await DataServiceConta.GetContasByCorrentista(id_conta);

            return contas;
        }
        private async void ListenForTransacao(int id_destinatario)
        {
            TransacaoCompleta ultima = await DataServiceTransacao.GetUltimaCompletaByDestinatario(id_destinatario);

            Correntista remetente;

            if (ultima != null)
            {
                await Task.Delay(500);
                ListenForTransacao(id_destinatario);
            }
            else
            {
                remetente = await DataServiceCorrentista.GetCorrentistaByID(ultima.Remetente.Id_Correntista);
                NotificationData notificacao = new NotificationData(
                    channelId: "transacoes",
                    title: "Nova transação",
                    message: $"{remetente.Nome.ToUpperInvariant()} te enviou {ultima.ValorFormatado} por {ultima.Remetente.TipoFormatado.ToUpperInvariant()}",
                    channelName: "ch_transacoes",
                    description: "Enviada quando você recebe uma transação"
                );

                AppServices.SendNotificationRequest(notificacao);

                return;
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
