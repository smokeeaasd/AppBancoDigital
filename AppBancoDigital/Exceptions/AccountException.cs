using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Exceptions
{
    [Serializable]
    public class AccountException : Exception
    {
        public AccountException() { }
        public AccountExceptionCode Code { get; set; }

        public AccountException(string message, AccountExceptionCode code)
            : base(String.Format("Erro de conta: {0}", message))
        {
            Code = code;
        }
    }

    public enum AccountExceptionCode
    {
        AccountExists,
        AccountNotExists,
        IncorrectCredentials
    }
}
