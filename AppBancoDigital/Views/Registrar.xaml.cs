using AppBancoDigital.Controls;
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
        private Element[] FormElements;

        public Registrar()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            FormElements = new Element[]
            {
                txt_confirmar_senha,
                txt_cpf,
                txt_data_nasc,
                txt_nome,
                txt_senha
            };
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
            if (!CheckForm()) return;

            if (txt_senha.Text != txt_confirmar_senha.Text)
            {
                await DisplayAlert("Problema ao cadastrar", "As senhas não coincidem", "OK");
                return;
            }
        }

        private string FormatCPF(string cpf)
        {
            string[] _cpf = cpf.Split(new char[] { '.', '-' });

            return string.Join("", _cpf);
        }

        private string GetDate(string date)
        {
            string[] _date = date.Split('/');

            string[] date_eng = _date.Reverse().ToArray();

            DateTime d = DateTime.Parse(string.Join("-", date_eng));

            return d.ToString();
        }

        private bool CheckForm()
        {
            foreach (Element elem in FormElements)
            {
                if (elem.GetType() == typeof(Entry) || elem.GetType() == typeof(MaskedEntry))
                {
                    if (((Entry)elem).Text == string.Empty)
                    {
                        ((Entry)elem).PlaceholderColor = Color.FromHex("#8a001c");
                        ((Entry)elem).Placeholder = "Esse campo é necessário";
                        return false;
                    }
                }
            }

            return true;
        }
    }
}