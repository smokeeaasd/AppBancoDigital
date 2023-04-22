using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBancoDigital.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicial : CarouselPage
    {
        public Inicial()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            Button btnProximo = page_welcome.FindByName<Button>("btn_proximo");

            btnProximo.Clicked += BtnProximo_Clicked;
        }

        private void BtnProximo_Clicked(object sender, EventArgs e)
        {
            ContentPage currentPage = CurrentPage;
            int nextPage = Children.IndexOf(currentPage) + 1;

            Children.Add(new Entrar());

            CurrentPage = Children[nextPage];

            Children.Remove(page_welcome);
        }
    }
}