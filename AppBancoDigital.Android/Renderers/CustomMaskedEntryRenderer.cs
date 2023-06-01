using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AppBancoDigital.Controls;
using AppBancoDigital.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MaskedEntry), typeof(CustomMaskedEntryRenderer))]
namespace AppBancoDigital.Droid.Renderers
{
    public class CustomMaskedEntryRenderer : EntryRenderer
    {
        MaskedEntry entry;
        public CustomMaskedEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            entry = Element as MaskedEntry;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var nativeEditText = (EditText)Control;
            var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
            shape.Paint.Color = entry.BorderColor.ToAndroid(); //entry.BorderColor is the 'BorderColor' property of the custom entry class  
            shape.Paint.SetStyle(Paint.Style.Stroke);
            shape.Paint.StrokeWidth = 10;
            nativeEditText.Background = shape;
        }
    }
}