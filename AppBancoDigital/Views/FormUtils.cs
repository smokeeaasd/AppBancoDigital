using System.Linq;
using System;

using Xamarin.Forms;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.Core;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.UI;
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
}