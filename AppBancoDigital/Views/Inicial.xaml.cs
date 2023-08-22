using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Extensions;
namespace AppBancoDigital.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicial : ContentPage
    {
        public Inicial()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

		private async void LoginClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Views.Login());
        }

		private async void RegistrarClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Views.Registrar());
		}
    }
}