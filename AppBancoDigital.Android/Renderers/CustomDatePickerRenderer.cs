using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Xaml;
using AppBancoDigital.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AppBancoDigital.Droid.Renderers;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace AppBancoDigital.Droid.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        private Context ctx;

        public CustomDatePickerRenderer(Context ctx) : base(ctx)
        {
            this.ctx = ctx;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.Transparent, Android.Graphics.PorterDuff.Mode.SrcAtop);
        }
    }
}