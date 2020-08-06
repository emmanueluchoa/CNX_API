using CNX_Domain.Entities.Enums;
using CNX_Domain.Interfaces.Application;
using CNX_Domain.Interfaces.Services;
using CNX_Domain.Models;
using System;

namespace CNX_Domain.Application
{
    public class PlaylistApplication : IPlaylistApplication
    {
        private readonly IUserApplication _userApplication;
        private readonly IWeatherApi _openWeatherService;
        private readonly IMusicApi _spotifyService;

        public PlaylistApplication(IUserApplication userApplication, IWeatherApi openWeatherService, IMusicApi playlistService)
        {
            this._userApplication = userApplication;
            this._openWeatherService = openWeatherService;
            this._spotifyService = playlistService;
        }

        public PlaylistVM GetPlaylistByWeatherLocalityCondition(string locale)
        {
            ValidateLocale(locale);
            int temperature = this._openWeatherService.GetTemperatureByLocation(locale);

            EnumPlayLists recomendedPlaylist = GetSugestedPlaylistByTemperature(temperature);

            return this._spotifyService.GetPlayList(recomendedPlaylist);
        }

        private static void ValidateLocale(string locale)
        {
            if (string.IsNullOrWhiteSpace(locale))
                throw new Exception("Locale not provided!");
        }

        public EnumPlayLists GetSugestedPlaylistByTemperature(int temperature)
        {
            EnumPlayLists playlist = EnumPlayLists.CLASSICAL;

            if (CheckIfPartyPlalist(temperature))
                playlist = EnumPlayLists.PARTY;

            else if (CheckIfPopPlaylist(temperature))
                playlist = EnumPlayLists.POP;

            else if (CheckIfRockPlaylist(temperature))
                playlist = EnumPlayLists.ROCK;

            return playlist;
        }

        private static bool CheckIfPartyPlalist(int temperature) =>
            30 < temperature;

        private static bool CheckIfRockPlaylist(int temperature) =>
            10 <= temperature && 14 >= temperature;

        private static bool CheckIfPopPlaylist(int temperature) =>
            15 <= temperature && 30 >= temperature;
    }
}
