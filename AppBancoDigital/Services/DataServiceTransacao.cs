using AppBancoDigital.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppBancoDigital.Services
{
	public class DataServiceTransacao : DataService
	{
		public static async Task<List<Transacao>> GetTransacoesByIdRemetente(int id_remetente)
		{
			string response = await GetDataFromService($"/api/transacao/by-remetente?id_remetente={id_remetente}");

			Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<List<Transacao>>(res.Data.ToString());
                case 2:
                    return null;
                default:
                    throw new Exception();
            }			
		}

		public static async Task<Transacao> GetTransacaoById(int id)
		{
			string response = await GetDataFromService($"/api/transacao/by-id?id={id}");

			Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<Transacao>(res.Data.ToString());
                case 2:
                    return null;
                default:
                    throw new Exception();
            }			
		}

		public static async Task<List<Transacao>> GetTransacoesByIdDestinatario(int id_destinatario)
		{
			string response = await GetDataFromService($"/api/transacao/by-destinatario?id_destinatario={id_destinatario}");

			Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<List<Transacao>>(res.Data.ToString());
                case 2:
                    return null;
                default:
                    throw new Exception();
            }			
		}

		public static async Task<List<Transacao>> OrdenarTransacoes(int id_conta)
		{
			List<Transacao> transacoes = new List<Transacao>();

			List<Transacao> transacoes_remetente = await GetTransacoesByIdRemetente(id_conta);
			List<Transacao> transacoes_destinatario = await GetTransacoesByIdDestinatario(id_conta);

			transacoes.AddRange(transacoes_remetente);
			transacoes.AddRange(transacoes_destinatario);

			transacoes.Sort((x, y) => {
				return DateTime.Compare(x.Data_Transacao, y.Data_Transacao);
			});

			return transacoes;
		}

		public static async Task<List<TransacaoCompleta>> GetTransacoesCompletas(int id_conta)
		{
			List<Transacao> transacoes_raw = await DataServiceTransacao.OrdenarTransacoes(id_conta);

			List<TransacaoCompleta> transacoes = new List<TransacaoCompleta>();

			if (transacoes_raw != null)
			{
				foreach (Transacao t in transacoes_raw)
				{
					transacoes.Add(new TransacaoCompleta() {
						Data_Transacao = t.Data_Transacao,
						Id = t.Id,
						Destinatario = await DataServiceConta.GetContaById(t.Id_Destinatario),
						Remetente = await DataServiceConta.GetContaById(t.Id_Remetente),
						Valor = t.Valor,
					});
				}

				return transacoes;
			}
			else return null;
		}
	}
}
