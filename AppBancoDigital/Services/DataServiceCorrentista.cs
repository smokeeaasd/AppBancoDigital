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

            return obj;
        }
    }
}
