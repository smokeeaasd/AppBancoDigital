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

        public string Senha { get; set; }

        public double Saldo { get; set; }
        public int Id_Correntista { get; set; }
    }
}
