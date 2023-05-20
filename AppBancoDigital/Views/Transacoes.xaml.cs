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
		public ObservableCollection<TransacaoCompleta> transacoes = new ObservableCollection<TransacaoCompleta>();
		public Conta conta;

		public Transacoes()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			BindableLayout.SetItemsSource(stack_transacoes, transacoes);
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			conta = BindingContext as Conta;
			transacoes.Clear();
			try
			{
				List<TransacaoCompleta> transacoes_list = await DataServiceTransacao.GetTransacoesCompletas(conta.Id);

				foreach (TransacaoCompleta tc in transacoes_list)
				{
					transacoes.Add(tc);
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert(ex.Message, ex.StackTrace, "ok");
			}
		}
	}
}