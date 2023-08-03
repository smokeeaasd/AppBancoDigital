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
using Xamarin.Essentials;

namespace AppBancoDigital
{
    public partial class App : Application
    {
		private delegate void TransacaoDelegate(Correntista remetente, Correntista destinatario, TransacaoCompleta transacao);
        private event TransacaoDelegate OnTransacaoRecebida;
        public App()
        {
			Device.SetFlags(new string[] { "AppTheme_Experimental" });
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            InitializeComponent();

			if (App.Current.Properties.ContainsKey("id_correntista"))
			{
                int id_correntista = int.Parse(App.Current.Properties["id_correntista"].ToString());

				if (Connectivity.NetworkAccess == NetworkAccess.Internet)
				{
					OnTransacaoRecebida += EnviarNotificacaoTransacao;
					ListenForTransacoes(id_correntista);
				}

            	MainPage = new NavigationPage(new Views.Home()
				{
					BindingContext = App.Current.Properties["id_correntista"] as int?
				});
			}
			else
			{
				MainPage = new NavigationPage(new Views.Inicial());
			}
        }

		private async void ListenForTransacoes(int id_correntista)
		{
			List<Conta> contas = await DataServiceConta.GetContasByCorrentista(id_correntista);

			if (contas != null)
			{
				foreach (Conta c in contas)
				{
					await ListenForTransacoesByConta(c.Id);
				}
			}
		}

		private List<int> recebidas = new List<int>();
		private async Task ListenForTransacoesByConta(int id_conta)
		{
			await Task.Delay(500);

			TransacaoCompleta transacao = await DataServiceTransacao.GetUltimaCompletaByDestinatario(id_conta);

			if (transacao != null && !recebidas.Contains(transacao.Id))
			{
				recebidas.Add(transacao.Id);
				Correntista remetente = await DataServiceCorrentista.GetCorrentistaByID(transacao.Remetente.Id_Correntista);
				Correntista destinatario = await DataServiceCorrentista.GetCorrentistaByID(transacao.Destinatario.Id_Correntista);

				OnTransacaoRecebida?.Invoke(remetente, destinatario, transacao);
			}
			await ListenForTransacoesByConta(id_conta);
		}

		private void EnviarNotificacaoTransacao(Correntista correntistaRemetente, Correntista correntistaDestinatario, TransacaoCompleta transacao)
		{
			NotificationData data = new NotificationData(
				"nova_transacao",
				"Transação",
				$"{correntistaRemetente.Nome} TE ENVIOU {transacao.ValorFormatado} VIA {transacao.Remetente.TipoFormatado}".ToUpperInvariant(),
				"channel_transacoes",
				"Notificação enviada quando o usuário recebe uma transação."
			);

			AppServices.SendNotificationRequest(data);
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