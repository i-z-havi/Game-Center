using Game_Center.Projects.CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Game_Center.Projects.CurrencyConverter.Services
{
    class CurrencyService
    {
        private const string BaseApiEndPoint = "http://api.exchangeratesapi.io/v1/latest";
        private const string ApiKey = "test test test 1 2 3 ";
        private HttpClient Http_Client= new HttpClient();

        //Tasks are asynchronous functions that return a value, 
        //the syntax is async Task<var type>
        public async Task<Dictionary<string,double>> GetExchangeRateAsync()
        {
            string requestUrl= $"{BaseApiEndPoint}?access_key={ApiKey}";
            string response = await Http_Client.GetStringAsync(requestUrl); //Http_Client =object we made on line 14
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            ExchangeResponse exchangeData= JsonSerializer.Deserialize<ExchangeResponse>(response,options);
            //in our exchange response object, properties are all properly capitalized. in the response, they're all lower.
            //this makes our request match our response.
            if (exchangeData == null || exchangeData.Rates == null)
                throw new InvalidOperationException("Failed to fetch exchange rates");
            return exchangeData.Rates;
        }
    }
}
