using AppBancoDigital.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancoDigital.Services;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Extensions;

namespace AppBancoDigital.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AreaPix : ContentPage
	{
		public AreaPix()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}