using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Models
{
    public class TransacaoCompleta
    {
        public int Id { get; set; }
		
        public DateTime Data_Transacao { get; set; }
		public Decimal Valor { get; set; }

		public Conta Remetente { get; set; }
		public Conta Destinatario { get; set; }

		public string ValorFormatado => Valor.ToString("C");
    }
}
