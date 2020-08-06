using CNX_Domain.Models;

namespace CNX_Domain.Interfaces.Services
{
    public interface IWeatherApi
    {
        int GetTemperatureByLocation(string location);
    }
}
