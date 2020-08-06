using RestSharp;

namespace CNX_Services
{
    public class OpenWeatherService
    {
        private readonly string url = "api.openweathermap.org/data/2.5/weather";
        public string GetWeather(string locale)
        {
            var client = new RestClient($"{url}?q={locale}&appid=7d2be348baaa48e6da3614b9ecb9711c&units=metric");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
    }
}
