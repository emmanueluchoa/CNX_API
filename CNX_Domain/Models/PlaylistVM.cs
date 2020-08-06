using System.Collections.Generic;
using System.Linq;

namespace CNX_Domain.Models
{
    public class PlaylistVM
    {
        public string Url { get; private set; }
        public string Name { get; private set; }
        public IList<TrackVM> Tracks { get; private set; }

        public PlaylistVM() =>
            CreateNewPlaylist();

        public PlaylistVM(string playlistName, IList<TrackVM> playlistTracks, string playlistUrl)
        {
            SetPlaylistName(playlistName);
            SetPlaylistTracks(playlistTracks);
            SetPlaylistUrl(playlistUrl);
        }

        public void SetPlaylistName(string playlistName) =>
            this.Name = playlistName;

        public void SetPlaylistTracks(IList<TrackVM> playlistTracks)
        {
            CreateNewPlaylist();

            if (null != playlistTracks && playlistTracks.Any())
                this.Tracks = playlistTracks;
        }

        public void SetPlaylistUrl(string playlistUrl) =>
            this.Url = playlistUrl;

        private void CreateNewPlaylist()
        {
            if (null == this.Tracks)
                this.Tracks = new List<TrackVM>();
        }
    }
}
