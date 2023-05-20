using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancoDigital.Models
{
    public class Transacao
    {
        public int Id { get; set; }
		
        public DateTime Data_Transacao { get; set; }
		public Decimal Valor { get; set; }

		public int Id_Remetente { get; set; }
		public int Id_Destinatario { get; set; }
    }
}
