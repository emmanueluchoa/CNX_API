using CNX_Domain.Models;

namespace CNX_Domain.Interfaces.Application
{
    public interface IPlaylistApplication
    {
        PlaylistVM GetPlaylistByWeatherLocalityCondition(string locale);
    }
}
