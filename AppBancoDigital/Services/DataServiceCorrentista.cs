using AppBancoDigital.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppBancoDigital.Exceptions;
using Xamarin.Forms;

namespace AppBancoDigital.Services
{
    public class DataServiceCorrentista : DataService
    {
        public static async Task<Correntista> InsertCorrentista(Correntista correntistaModel)
        {
            string json = JsonConvert.SerializeObject(correntistaModel);

            Console.WriteLine(json);
            
            string response = await PostDataToService(json, "/api/correntista/new");

            Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<Correntista>(res.Data.ToString());
                case 2:
                    throw new AccountException("Já existe uma conta com o CPF informado.", AccountExceptionCode.AccountExists);
                default:
                    throw new Exception();
            }
        }

        public static async Task<Correntista> GetCorrentistaByCpfAndSenha(Correntista correntistaModel, Page page)
        {
            string json = JsonConvert.SerializeObject(correntistaModel);

            Console.WriteLine(json);

            string cpf = correntistaModel.CPF;
            string senha = correntistaModel.Senha;

            string response = await GetDataFromService($"/api/correntista/connect?cpf={cpf}&senha={senha}");

            Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<Correntista>(res.Data.ToString());
                case 2:
                    throw new AccountException("Credenciais incorretas", AccountExceptionCode.IncorrectCredentials);
                default:
                    throw new Exception();
            }
        }

        public static async Task<Correntista> GetCorrentistaByID(int id)
        {
            string response = await GetDataFromService($"/api/correntista/by-id?id={id}");

            Result res = JsonConvert.DeserializeObject<Result>(response);

            switch (res.Type)
            {
                case 1:
                    return JsonConvert.DeserializeObject<Correntista>(res.Data.ToString());
                case 2:
                    throw new AccountException("Conta não existe", AccountExceptionCode.AccountNotExists);
                default:
                    throw new Exception();
            }
        }
    }
}
