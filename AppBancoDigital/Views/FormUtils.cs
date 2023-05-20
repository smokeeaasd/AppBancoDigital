using System.Linq;
using System;

using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;

namespace AppBancoDigital.Views
{
	public class FormUtils
	{
		public static string ParseDate(DateTime date)
		{
			string[] dt_arr = date.ToShortDateString().Split('/');

			return string.Join("-", dt_arr.Reverse());
		}

		public static string FormatCPF(string cpf)
		{
			string[] _cpf = cpf.Split(new char[] { '.', '-' });

			return string.Join("", _cpf);
		}

	}

	public class PageUtils
	{
		public static Popup MakeAlertPopup(Size size, string title, string message)
		{
			Popup popup = new Popup()
			{
				Size = size,
				Content = new StackLayout
				{
					BackgroundColor = Color.FromHex("#000000"),
					Children =
					{
						new Frame {
							BackgroundColor = Color.FromHex("#db0063"),
							HeightRequest = 10
						},
						new Label {
							Text = title,
							FontFamily = "StellaNova",
							HorizontalOptions = LayoutOptions.Center,
							Margin = new Thickness(0, 20),
							TextColor = Color.FromHex("#ffffff"),
							FontSize = 25
						},
						new Label {
							Text = message,
							FontFamily = "OpenSans",
							HorizontalOptions = LayoutOptions.Center,
							Margin = new Thickness(20, 0),
							TextColor = Color.FromHex("#ffffff"),
							FontSize = 20
						}
					}
				},
			};

			return popup;
		}

		public static Popup MakeConfirmationPopup(string title, string caption, string accept, string refuse)
		{
			Grid grid = new Grid();

			Button acceptButton = new Button {
				BackgroundColor = Color.Transparent,
				Text = accept,
				FontFamily = "OpenSans",
				FontSize = 20,
				TextColor = Color.FromHex("#db0063"),
				HorizontalOptions = LayoutOptions.Center,
				TextTransform = TextTransform.None,
			};

			Button refuseButton = new Button {
				BackgroundColor = Color.Transparent,
				Text = refuse,
				FontFamily = "OpenSans",
				FontSize = 20,
				TextColor = Color.FromHex("#db0063"),
				HorizontalOptions = LayoutOptions.Center,
				TextTransform = TextTransform.None,
			};

			grid.Children.Add(refuseButton, 0, 0);
			grid.Children.Add(acceptButton, 1, 0);

			Popup popup = new Popup()
			{
				Size = new Size(310, 225),
				Content = new StackLayout
				{
					BackgroundColor = Color.FromHex("#070707"),
					Children = {
						new Label {
							Text = title,
							FontFamily = "OpenSans",
							TextColor = Color.FromHex("#ffffff"),
							FontSize = 25,
							Margin = new Thickness(20, 20)
						},
						new Label {
							Text = caption,
							FontFamily = "OpenSans",
							TextColor = Color.FromHex("#ffffff"),
							FontSize = 20,
							Margin = new Thickness(20, 15)
						},
						grid
					}
				},
			};

			acceptButton.Clicked += delegate {
				popup.Dismiss(true);
			};
			
			refuseButton.Clicked += delegate {
				popup.Dismiss(false);
			};

			return popup;
		}
	}
}