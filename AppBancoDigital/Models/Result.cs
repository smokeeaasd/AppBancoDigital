using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Models
{
    public class Result
    {
        public int Type { get; set; }
        public object Data { get; set; }

        public Result(int type, object data)
        {
            Type = type;
            Data = data;
        }
    }
}
