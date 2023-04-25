using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Models
{
    public class CorrentistaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Senha { get; set; }
    }
}
