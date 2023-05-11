using AppBancoDigital.Exceptions;
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

            try
            {
                Correntista correntista = await DataServiceCorrentista.GetCorrentistaByCpfAndSenha(new Correntista
                {
                    CPF = cpf,
                    Senha = senha
                }, this);

                await Navigation.PushAsync(new Views.Home()
                {
                    BindingContext = await DataServiceCorrentista.GetCorrentistaByID(correntista.Id)
                });
            }
            catch (AccountException ex)
            {
                switch (ex.Code)
                {
                    case APIGetDataExceptionType.DataExists:
                        lbl_incorrect.IsVisible = true;
                    break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(ex.Message, ex.StackTrace, "OK");
            }
        }
    }
}