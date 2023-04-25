using AppBancoDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBancoDigital.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrar : ContentPage
    {
        public Registrar()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void ClearControls()
        {
            txt_confirmar_senha.Text = string.Empty;
            txt_cpf.Text = string.Empty;
            txt_data_nasc.Text = string.Empty;
            txt_nome.Text = string.Empty;
            txt_senha.Text = string.Empty;
        }

        private async void CriarContaClicked(object sender, EventArgs e)
        {
            if (txt_senha.Text != txt_confirmar_senha.Text)
            {
                await DisplayAlert("Problema ao cadastrar", "As senhas não coincidem", "OK");
                return;
            }
        }
    }
}