using CNX_Domain.Interfaces.Services;
using CNX_Domain.Models.WeatherMapService;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace CNX_Domain.Services
{
    public class OpenWeatherService : IWeatherApi
    {
        private readonly string _url = "http://api.openweathermap.org/data/2.5/weather";
        private readonly string _openWeatherApiKey = "7d2be348baaa48e6da3614b9ecb9711c";
        public int GetTemperatureByLocation(string locale)
        {
            string url = $@"{this._url}?q={locale}&appid={this._openWeatherApiKey}&units=metric";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                WeatherServiceVM weatherData = JsonConvert.DeserializeObject<WeatherServiceVM>(response.Content);
                ValidateWeatherData(weatherData);

                return weatherData.Main.temp;
            }
            else
                throw new Exception(response.Content);
        }

        private static void ValidateWeatherData(WeatherServiceVM weatherData)
        {
            if (null == weatherData || null == weatherData.Main)
                throw new Exception("Weather data not found.");
        }
    }
}
