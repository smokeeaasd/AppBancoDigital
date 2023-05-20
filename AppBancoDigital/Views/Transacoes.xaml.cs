using AppBancoDigital.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using AppBancoDigital.Services;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Extensions;

namespace AppBancoDigital.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Transacoes : ContentPage
	{
		public ObservableCollection<TransacaoCompleta> transacoes;
		public Conta conta;

		public Transacoes()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			BindableLayout.SetItemsSource(stack_transacoes, transacoes);

			LogService.Instance.Send("Testando", "Oi");
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			conta = BindingContext as Conta;

			try
			{
				List<TransacaoCompleta> transacoes_list = await DataServiceTransacao.GetTransacoesCompletas(conta.Id);
			}
			catch (Exception ex)
			{
				await DisplayAlert(ex.Message, ex.StackTrace, "ok");
			}
		}
	}
}