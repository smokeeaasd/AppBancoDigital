using AppBancoDigital.Models;
using AppBancoDigital.Services;
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LogarClicked(object sender, EventArgs e)
        {
            string cpf = FormUtils.FormatCPF(txt_cpf.Text);
            string senha = txt_senha.Text;

            object res = await DataServiceCorrentista.GetCorrentistaByCpfAndSenha(new Correntista
            {
                CPF = cpf,
                Senha = senha
            });

            if (res != null)
            {
                Correntista c = res as Correntista;

                await DisplayAlert("Oi", c.data_nasc, "ok");
            }

        }
    }
}