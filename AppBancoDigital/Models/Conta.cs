using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }

        public int Senha { get; set; }

        public int IdCorrentista { get; set; }
    }
}
