using AppBancoDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancoDigital.Services;
using Xamarin.CommunityToolkit.Extensions;

namespace AppBancoDigital.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private Correntista correntista;
        public Home()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            img_no_account.Source = ImageSource.FromResource("AppBancoDigital.Assets.Images.money.png");
        }

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

        private void CriarContaClicked(object sender, EventArgs e)
        {
            Navigation.ShowPopupAsync(new CriarConta());
        }

        private void LoadAccountCards(List<Conta> contas)
        {
            foreach (Conta conta in contas)
            {
                stack_principal.Children.Add(MakeAccountCard(conta));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            correntista = BindingContext as Correntista;

            List<Conta> contas = await DataServiceConta.GetContasByCorrentista(correntista.Id);

            if (contas != null)
                LoadAccountCards(contas);
        }
    }
}