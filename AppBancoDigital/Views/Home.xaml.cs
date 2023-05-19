using AppBancoDigital.Models;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancoDigital.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using System.Collections.Specialized;

namespace AppBancoDigital.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		ObservableCollection<Conta> contas = new ObservableCollection<Conta>();

		private Correntista correntista;

		public Home()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			img_no_account.Source = ImageSource.FromResource("AppBancoDigital.Assets.Images.money.png");
			stack_contas.BindingContext = this;
			BindableLayout.SetItemsSource(stack_contas, contas);
			contas.CollectionChanged += ContasListUpdate;
		}

		private void ContasListUpdate(object sender, NotifyCollectionChangedEventArgs e)
		{
			foreach (Conta c in contas)
			{
				string tipo = c.Tipo.Substring(0, 1).ToUpperInvariant() + c.Tipo.Substring(1).ToLowerInvariant();

				contas[contas.IndexOf(c)].Tipo = tipo;
			}
		}

		/**
        private Frame MakeAccountCard(Conta c)
        {
            Grid grid = new Grid();

            grid.Children.Add(new Label()
            {
                Text = c.Tipo,
                FontSize = 20,
                FontFamily = "StellaNova",
                TextColor = Color.FromHex("#eee")
            }, 0, 0);

            grid.Children.Add(new Label()
            {
                Text = c.Numero.ToString(),
                FontSize = 20,
                FontFamily = "StellaNova",
                TextColor = Color.FromHex("#eee"),
                HorizontalOptions = LayoutOptions.End
            }, 1, 0);

            Frame frm = new Frame()
            {
                BackgroundColor = Color.FromHex("#00cc73"),
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 25, 0, 0),
                WidthRequest = 275,
                HeightRequest = 125,
                CornerRadius = 10,
                Content = new StackLayout()
                {
                    Children =
                    {
                        grid,
                        new Label() {
                            Text = c.Saldo.ToString("C"),
                            FontSize = 25,
                            FontFamily = "StellaNova",
                            TextColor = Color.FromHex("#eee"),
                            Margin = new Thickness(0, 10)
                        }
                    }
                }
            };

            return frm;
        }
		*/

		private async void CriarContaClicked(object sender, EventArgs e)
		{
			object res = await Navigation.ShowPopupAsync(new CriarConta()
			{
				BindingContext = await DataServiceConta.GetContasByCorrentista(correntista.Id)
			});
			if (res is null) return;
			try
			{
				Conta c = res as Conta;

				c.Id_Correntista = correntista.Id;
				c.Saldo = 0;

				await DataServiceConta.InsertConta(c);

				App.Current.MainPage = new Home()
				{
					BindingContext = await DataServiceCorrentista.GetCorrentistaByID(correntista.Id)
				};
			}
			catch (Exception ex)
			{
				await DisplayAlert(ex.Message, ex.StackTrace, "ok");
			}
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			this.contas.Clear();

			correntista = BindingContext as Correntista;

			try
			{
				act_carregando.IsVisible = true;
				List<Conta> contas = await DataServiceConta.GetContasByCorrentista(correntista.Id);

				stack_no_account.IsVisible = contas == null;

				if (contas != null)
				{
					foreach (Conta c in contas)
					{
						this.contas.Add(c);
					}
				}
				act_carregando.IsVisible = false;
			}
			catch (Exception ex)
			{
				await DisplayAlert(ex.Message, ex.StackTrace, "OK");
			}
			finally
			{
				act_carregando.IsVisible = false;
			}

		}
	}
}