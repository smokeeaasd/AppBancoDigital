using AppBancoDigital.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppBancoDigital.Exceptions;

namespace AppBancoDigital.Services
{
    public class DataServiceCorrentista : DataService
    {
        public static async Task<object> InsertCorrentista(Correntista correntistaModel)
        {
            string json = JsonConvert.SerializeObject(correntistaModel);

            Console.WriteLine(json);
            
            string response = await PostDataToService(json, "/api/correntista/new");

            var obj = JsonConvert.DeserializeObject<Correntista>(response);

            if (obj == null)
                throw new AccountException("Conta já existe", AccountExceptionCode.AccountExists);

            return obj;
        }

        public static async Task<object> GetCorrentistaById(Correntista correntistaModel)
        {
            string json = JsonConvert.SerializeObject(correntistaModel);

            Console.WriteLine(json);

            string cpf = correntistaModel.CPF;
            string senha = correntistaModel.Senha;

            string response = await GetDataFromService($"/api/correntista/connect?cpf={cpf}&senha={senha}");

            var obj = JsonConvert.DeserializeObject<Correntista>(response);

            if (obj == null)
                throw new AccountException("Não foi encontrada conta com os dados informados", AccountExceptionCode.IncorrectCredentials);

            return obj;
        }
    }
}
