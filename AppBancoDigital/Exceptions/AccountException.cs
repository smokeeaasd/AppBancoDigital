using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Exceptions
{
    [Serializable]
    public class AccountException : Exception
    {
        public AccountException() { }
        public int Code { get; set; }

        public AccountException(string message, int number)
            : base(String.Format("Erro de conta: {0}", message))
        {
            Code = number;
        }
    }
}
