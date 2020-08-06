using CNX_Domain.Entities.Enums;
using CNX_Domain.Interfaces.Services;
using CNX_Domain.Models;
using CNX_Domain.Models.SpotifyServices;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNX_Domain.Services
{
    public class SpotifyService : IMusicApi
    {
        private readonly string _spotifyClientId = "e1ee724863ef40a9b317f124860a32a9";
        private readonly string _spotifyClientSecret = "d6fdf05ef1ca47f1992571f1a9050b5e";
        private readonly string _spotifyTokenApiUrl = "https://accounts.spotify.com/api/token";
        private readonly string _spotifyPlayListApiUrl = "https://api.spotify.com/v1/playlists/";
        public PlaylistVM GetPlayList(EnumPlayLists playLists)
        {
            string spotifyToken = GetSpotifyToken();
            CheckIfSpotifyTokenHasBeenProvided(spotifyToken);

            var client = new RestClient(string.Concat(this._spotifyPlayListApiUrl, GetPlaylist(playLists)));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {spotifyToken}");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                SpotifyPlaylistVM spotifyPlaylist = JsonConvert.DeserializeObject<SpotifyPlaylistVM>(response.Content);
                IList<TrackVM> playListTracks = (from track in spotifyPlaylist.tracks.items
                                                 select new TrackVM(track.track.name, track.track.album.name, track.track.artists.Select(artist => artist.name).ToList(), track.track.external_urls.spotify, track.track.preview_url, track.track.duration_ms)).ToList();

                PlaylistVM recomendedPlaylist = new PlaylistVM();
                recomendedPlaylist.SetPlaylistTracks(playListTracks);
                recomendedPlaylist.SetPlaylistName(spotifyPlaylist.name);
                recomendedPlaylist.SetPlaylistUrl(spotifyPlaylist.external_urls.spotify);

                return recomendedPlaylist;
            }
            else
                throw new Exception(response.Content);
        }

        private static DateTime GetReleaseDate(Models.SpotifyServices.Item track)
        {
            DateTime releaseDate = DateTime.MinValue;

            if (!string.IsNullOrWhiteSpace(track.track.album.release_date))
                DateTime.TryParse(track.track.album.release_date, out releaseDate);

            return releaseDate;
        }

        private static string GetAlbumName(Models.SpotifyServices.Item track)
        {
            return track.track.album.album_type ?? string.Empty;
        }

        private static string GetArtists(Models.SpotifyServices.Item track)
        {
            return null != track.track && track.track.artists.Any() ? string.Join(", ", track.track.artists.Select(artist => artist.name)) : string.Empty;
        }

        private static void CheckIfSpotifyTokenHasBeenProvided(string spotifyToken)
        {
            if (string.IsNullOrWhiteSpace(spotifyToken))
                throw new Exception("Spotify token invalid or not provided.");
        }

        public string GetSpotifyToken()
        {
            var client = new RestClient(this._spotifyTokenApiUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.Default.GetBytes(string.Concat(this._spotifyClientId, ":", this._spotifyClientSecret)))}");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Cookie", "__Host-device_id=AQD_9_QRjHIrwLfJp6G4az56AZjeVKL6KS3stCibypLyo-SkVSDdelHDPJoK1D538TRRccknngfFdU_2CwM4dHz3Bbt2RK2uqic; __Secure-TPASESSION=AQDfwO6Pb23KClA+MrEAEqGV+6WUBeyTOaCnZ3iOaUiB6indJdVtPv+NyC8AcXooFUAKxgEY3vk0Ayb5SGZ53+qTizCjEFvcFH4=; csrf_token=AQBBsy-0d_fxC4Ms23Qg8ljGaPV53BJQnDfaCQiTxThlUFoG1YypslJgYNqonPTLmdbygB0QVzki6iM_");
            request.AddParameter("grant_type", "client_credentials");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                SpotifyTokenVM spotifyToken = JsonConvert.DeserializeObject<SpotifyTokenVM>(response.Content);
                return spotifyToken.access_token;
            }
            else
                throw new Exception(response.Content);
        }

        private string GetPlaylist(EnumPlayLists playLists)
        {
            string playlistPredicate = string.Empty;

            switch (playLists)
            {
                case EnumPlayLists.POP:
                    playlistPredicate = "37i9dQZF1DWTwnEm1IYyoj";
                    break;
                case EnumPlayLists.ROCK:
                    playlistPredicate = "37i9dQZF1DX6xOPeSOGone";
                    break;
                case EnumPlayLists.CLASSICAL:
                    playlistPredicate = "37i9dQZF1DWWEJlAGA9gs0";
                    break;
                default:
                    playlistPredicate = "37i9dQZF1DXaXB8fQg7xif";
                    break;
            }

            return playlistPredicate;
        }
    }
}
