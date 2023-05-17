using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Exceptions
{
    [Serializable]
    public class APIException : Exception
    {
        public APIExceptionType Code { get; set; }

        public APIException(string message, APIExceptionType code)
            : base(String.Format("Erro de conta: {0}", message))
        {
            Code = code;
        }
    }

    public enum APIExceptionType
    {
        DataExists,
        DataNotExists,
        IncorrectCredentials
    }
}
