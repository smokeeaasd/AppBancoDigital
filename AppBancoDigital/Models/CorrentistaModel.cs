using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Models
{
    public class Correntista
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string data_nasc { get; set; }

        public string Senha { get; set; }
    }
}
