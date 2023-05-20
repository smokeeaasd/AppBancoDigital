using AppBancoDigital.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancoDigital.Services;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Extensions;
using System.Threading.Tasks;

namespace AppBancoDigital.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		ObservableCollection<Conta> contas = new ObservableCollection<Conta>();

		public Correntista correntista { get; set; }

		public Conta corrente { get; set; }
		public Conta poupanca { get; set; }

		public Home()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
		
		private async void GetTransacoesCorrente(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new Transacoes() {
				BindingContext = corrente
			});
		}

		private async void GetTransacoesPoupanca(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new Transacoes() {
				BindingContext = poupanca
			});
		}

		private async void SairClicked(object sender, EventArgs e)
		{
			Popup popupSaindo = PageUtils.MakeConfirmationPopup("Sair da conta", "Você deseja encerrar a sessão atual?", "Sim", "Não");

			object result = await Navigation.ShowPopupAsync(popupSaindo);

			if (result is bool res)
			{
				if (res)
				{
					App.Current.Properties.Remove("id_correntista");
					Application.Current.MainPage = new NavigationPage(new Inicial());
				}
			}
		}

		bool hideCorrente = false;
		private void HandleSaldoCorrenteVisibility(object sender, EventArgs e)
		{
			hideCorrente = !hideCorrente;
			((Button)sender).Text = (hideCorrente) ? "\uE8f4" : "\uE8f5";
			saldo_corrente.Text = (hideCorrente) ? "R$ *****" : corrente.SaldoFormatado;
		}

		bool hidePoupanca = false;
		private void HandleSaldoPoupancaVisibility(object sender, EventArgs e)
		{
			hidePoupanca = !hidePoupanca;

			((Button)sender).Text = (hidePoupanca) ? "\uE8f4" : "\uE8f5";
			saldo_poupanca.Text = (hidePoupanca) ? "R$ *****" : poupanca.SaldoFormatado;
		}

		private async void CriarContaClicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new CriarConta()
			{
				BindingContext = correntista
			});

			Navigation.RemovePage(this);

			this.contas.Clear();
			CarregarContas();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			this.contas.Clear();

			int id_correntista = (int)BindingContext;
			
			correntista = await DataServiceCorrentista.GetCorrentistaByID(id_correntista);

			stack_principal.BindingContext = correntista;

			CarregarContas();
		}

		private async void CarregarContas()
		{
			try
			{
				List<Conta> contas = await DataServiceConta.GetContasByCorrentista(correntista.Id);

				if (contas != null)
				{
					corrente = contas.Find((c) => c.Tipo == "CORRENTE");
					poupanca = contas.Find((c) => c.Tipo == "POUPANCA");

					if (corrente != null)
					{
						card_corrente.BindingContext = corrente;
						card_corrente.IsVisible = true;
					}
					if (poupanca != null)
					{
						card_poupanca.BindingContext = poupanca;
						card_poupanca.IsVisible = true;
					}
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert(ex.Message, ex.StackTrace, "OK");
			}
		}
	}
}