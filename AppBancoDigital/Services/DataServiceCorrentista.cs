using AppBancoDigital.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppBancoDigital.Services
{
    public class DataServiceCorrentista : DataService
    {
        public static async Task<object> InsertCorrentista(CorrentistaModel correntistaModel)
        {
            string json = JsonConvert.SerializeObject(correntistaModel);

            string response = await PostDataToService(json, "/api/correntista/new");

            var obj = JsonConvert.DeserializeObject(response);

            CorrentistaModel correntista = obj as CorrentistaModel;

            if (correntista != null)
                return obj as CorrentistaModel;
            else
                throw new Exception("Algo está errado");
        }
    }
}
