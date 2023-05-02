﻿using AppBancoDigital.Controls;
using AppBancoDigital.Models;
using AppBancoDigital.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AppBancoDigital.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrar : ContentPage
    {
        private Entry[] FormElements;
        private string[] FormDefaultPlaceholders;
        private Color DefaultPlaceHolderColor;

        public Registrar()
        {
            InitializeComponent();
            DefaultPlaceHolderColor = txt_nome.PlaceholderColor;

            NavigationPage.SetHasNavigationBar(this, false);

            FormElements = new Entry[]
            {
                txt_confirmar_senha,
                txt_cpf,
                txt_nome,
                txt_senha
            };

            FormDefaultPlaceholders = new string[]
            {
                txt_confirmar_senha.Placeholder,
                txt_cpf.Placeholder,
                txt_nome.Placeholder,
                txt_senha.Placeholder,
            };

            DateTime now = DateTime.Now;

            DateTime maxDate = now.AddDays(-(365*18));

            dtpck_data_nasc.MaximumDate = maxDate;

            foreach (Entry elem in FormElements)
            {
                elem.Focused += delegate
                {
                    foreach (Entry entry in FormElements)
                    {
                        entry.Placeholder = FormDefaultPlaceholders.ElementAt(FormElements.IndexOf(entry));
                        entry.PlaceholderColor = DefaultPlaceHolderColor;
                    }
                };
            }
        }

        private void ClearControls()
        {
            txt_confirmar_senha.Text = string.Empty;
            txt_cpf.Text = string.Empty;
            txt_nome.Text = string.Empty;
            txt_senha.Text = string.Empty;
        }

        private async void CriarContaClicked(object sender, EventArgs e)
        {
            if (!CheckForm())
                return;

            if (txt_senha.Text != txt_confirmar_senha.Text)
            {
                await DisplayAlert("Problema ao cadastrar", "As senhas não coincidem", "OK");
                return;
            }

            Correntista correntista = new Correntista()
            {
                CPF = FormatCPF(txt_cpf.Text),
                data_nasc = ParseDate(dtpck_data_nasc.Date),
                Nome = txt_nome.Text,
                Senha = txt_senha.Text
            };

            try
            {
                Correntista c = await DataServiceCorrentista.InsertCorrentista(correntista) as Correntista;

                await DisplayAlert(c.data_nasc, c.data_nasc, "ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert(ex.Message, ex.StackTrace, "OK");
            }
        }

        private string ParseDate(DateTime date)
        {
            string[] dt_arr = date.ToShortDateString().Split('/');

            return string.Join("-", dt_arr.Reverse());
        }

        private string FormatCPF(string cpf)
        {
            string[] _cpf = cpf.Split(new char[] { '.', '-' });

            return string.Join("", _cpf);
        }

        private bool CheckForm()
        {
            bool isValid = true;

            foreach (Entry elem in FormElements)
            {
                if (string.IsNullOrWhiteSpace(elem.Text))
                {
                    isValid = false;
                    elem.PlaceholderColor = Color.FromHex("#8a001c");
                    elem.Placeholder = "Esse campo é necessário";
                }
            }

            return isValid;
        }
    }
}