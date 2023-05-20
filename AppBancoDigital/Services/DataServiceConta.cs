using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppBancoDigital.Exceptions;
using AppBancoDigital.Models;
using Newtonsoft.Json;

namespace AppBancoDigital.Services
{
    public class DataServiceConta : DataService
    {
		public static async Task<Conta> GetContaById(int id)
        {
            string response = await GetDataFromService($"/api/conta/by-id?id={id}");

            Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<Conta>(res.Data.ToString());
                case 2:
                    return null;
                default:
                    throw new Exception();
            }
        }

        public static async Task<List<Conta>> GetContasByCorrentista(int id_correntista)
        {
            string response = await GetDataFromService($"/api/conta/by-correntista?id={id_correntista}");

            Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<List<Conta>>(res.Data.ToString());
                case 2:
                    return null;
                default:
                    throw new Exception();
            }
        }

        public static async Task<Conta> InsertConta(Conta contaModel)
        {
            string json = JsonConvert.SerializeObject(contaModel);

            string response = await PostDataToService(json, "/api/conta/new");

            Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<Conta>(res.Data.ToString());
                default:
                    throw new Exception();
            }
        }
    }
}
