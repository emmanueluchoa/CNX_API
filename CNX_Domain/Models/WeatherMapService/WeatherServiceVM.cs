namespace CNX_Domain.Models.WeatherMapService
{
    public class WeatherServiceVM
    {
        public Main Main { get; set; }
    }
    public class Main
    {
        public int temp { get; set; }
        public double feels_like { get; set; }
        public int temp_min { get; set; }
        public int temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }
}
