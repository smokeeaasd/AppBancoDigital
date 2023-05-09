using System.Linq;
using System;

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