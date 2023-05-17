using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using AppBancoDigital.Models;
using AppBancoDigital.Services;

namespace AppBancoDigital.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriarConta : Popup
    {
        List<Conta> contas;

        public CriarConta()
        {
            InitializeComponent();
        }

        string selecionada;
        private void btn_poupanca_Clicked(object sender, EventArgs e)
        {
            btn_corrente.BackgroundColor = Color.FromHex("#6b6b6b");
            btn_poupanca.BackgroundColor = Color.FromHex("#00cc73");

            selecionada = "POUPANCA";
        }

        private void btn_corrente_Clicked(object sender, EventArgs e)
        {
            btn_poupanca.BackgroundColor = Color.FromHex("#6b6b6b");
            btn_corrente.BackgroundColor = Color.FromHex("#00cc73");

            selecionada = "CORRENTE";
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            contas = BindingContext as List<Conta>;

            bool temPoupanca;
            bool temCorrente;
            if (contas == null)
            {
                temPoupanca = false;
                temCorrente = false;
            }
            else
            {
                temPoupanca = contas.Exists((c) => c.Tipo == "POUPANCA");
                temCorrente = contas.Exists((c) => c.Tipo == "CORRENTE");
            }

            if (temCorrente)
                btn_corrente.IsVisible = false;

            if (temPoupanca)
                btn_poupanca.IsVisible = false;
        }

        private void btn_criar_conta_Clicked(object sender, EventArgs e)
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
                    Tipo = selecionada,
                    Senha = txt_senha.Text
                };

                Dismiss(c);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}