using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancoDigital.Models;
using AppBancoDigital.Services;

namespace AppBancoDigital.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriarConta : ContentPage
    {
        List<Conta> contas;
		Correntista correntista;

        public CriarConta()
        {
            InitializeComponent();
        }

        string selecionada;
        private void btn_poupanca_Clicked(object sender, EventArgs e)
        {
            btn_corrente.BackgroundColor = Color.FromHex("#6b6b6b");
            btn_poupanca.BackgroundColor = Color.FromHex("#007a45");

            selecionada = "POUPANCA";
        }

        private void btn_corrente_Clicked(object sender, EventArgs e)
        {
            btn_poupanca.BackgroundColor = Color.FromHex("#6b6b6b");
            btn_corrente.BackgroundColor = Color.FromHex("#007a45");

            selecionada = "CORRENTE";
        }

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			correntista = BindingContext as Correntista;
			contas = await DataServiceConta.GetContasByCorrentista(correntista.Id);

			if (contas != null)
			{
				btn_corrente.IsVisible = !contas.Exists((c) => c.Tipo == "CORRENTE");
				btn_poupanca.IsVisible = !contas.Exists((c) => c.Tipo == "POUPANCA");
			} else {
				btn_corrente.IsVisible = true;
				btn_poupanca.IsVisible = true;
			}

		}

        private async void btn_criar_conta_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (txt_senha.Text != txt_confirmar_senha.Text)
                {
                    senha_diferente.IsVisible = true;
                    return;
                }
                if (selecionada == null)
                {
                    nenhum_selecionado.IsVisible = true;
                    return;
                }

                Conta c = new Conta()
                {
					Id_Correntista = correntista.Id,
                    Tipo = selecionada,
					Saldo = 0,
                    Senha = txt_senha.Text
                };

				await DataServiceConta.InsertConta(c);

				await Navigation.PopModalAsync();
            } catch (Exception ex)
            {
                await DisplayAlert(ex.Message, ex.StackTrace, "OK");
            }
        }
    }
}