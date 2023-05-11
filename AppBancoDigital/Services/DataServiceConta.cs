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
        public static async Task<List<Conta>> GetContasByCorrentista(int id_correntista)
        {
            string response = await GetDataFromService($"/api/conta/by-correntista?id={id_correntista}");

            Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<List<Conta>>(res.Data.ToString());
                case 2:
                    throw new AccountException("Não tem conta :o", APIGetDataExceptionType.DataNotExists);
                default:
                    throw new Exception();
            }
        }
    }
}
