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
    public partial class Inicial : ContentPage
    {
        private Button btnVoltar;
        private Button btnProximo;
        public Inicial()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            btnVoltar = inicial1.FindByName("voltar") as Button;
            btnProximo = inicial1.FindByName("proximo") as Button;

            btnVoltar.Clicked += BtnVoltar_Clicked;

            btnProximo.Clicked += BtnProximo_Clicked;
        }

        private void BtnProximo_Clicked(object sender, EventArgs e)
        {
            inicial1.IsVisible = true;
            inicial2.IsVisible = false;
        }

        private void BtnVoltar_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}