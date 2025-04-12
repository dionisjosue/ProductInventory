using System;
using System.Text.Json;
using Infrastructure.IServices;

namespace Infrastructure.Services
{
	public class RateService:IRateService
	{

        private HttpClient httpClient;

		public RateService()
		{
		}

        public async Task<decimal> GetCurrencyValueAsync(string moneda)
        {
            decimal currencyVal = 1;
            try { 
                var client = new HttpClient();
                var apiKey = Environment.GetEnvironmentVariable("rateApiKey");
                var url = Environment.GetEnvironmentVariable("rateUrl");

                url = url.Replace("{APIKEY}", apiKey);
                url = url.Replace("{moneda}", moneda);


                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var exchangeRate = JsonSerializer.Deserialize<ExchangeRateResponse>(content, options);

                if (exchangeRate != null && exchangeRate.ConversionRates != null)
                {
                    currencyVal = exchangeRate.ConversionRates["DOP"];

                }
            }
            catch(Exception ex)
            {

            }
            return currencyVal;
        }

        public decimal GetPriceConverted(decimal rate, decimal price)
        {
            return price / rate;
        }
    }
}

