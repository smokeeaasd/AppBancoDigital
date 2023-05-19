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

		public Correntista correntista { get; set; }

		public Home()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
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
				act_carregando.IsVisible = true;
				act_carregando.IsRunning = true;
				List<Conta> contas = await DataServiceConta.GetContasByCorrentista(correntista.Id);

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
				act_carregando.IsRunning = false;
			}
		}
	}
}