using System;
using System.Collections.Generic;
using System.Linq;

namespace CNX_Domain.Models
{
    public class TrackVM
    {
        public string Name { get; private set; }
        public string Artists { get; private set; }
        public string Url { get; private set; }
        public string UrlPreview { get; private set; }
        public string Album { get; private set; }
        public TimeSpan? Duration { get; private set; }

        public TrackVM() { }

        public TrackVM(string trackName, string albumName, IList<string> artists, string trackUrl, string trackUrlPreview, double duration)
        {
            SetTrackName(trackName);
            SetTrackArtists(artists);
            SetTrackUrl(trackUrl);
            SetTrackUrlPreview(trackUrlPreview);
            SetAlbumName(albumName);
            SetTrackDuration(duration);
        }

        public void SetTrackName(string trackName) =>
            this.Name = trackName;
        public void SetTrackArtists(IList<string> artistList)
        {
            string artists = string.Empty;

            if (null != artistList && artistList.Any())
                artists = string.Join(", ", artistList);

            this.Artists = artists;
        }

        public void SetTrackUrl(string trackUrl) =>
          this.Url = trackUrl;

        public void SetTrackUrlPreview(string trackUrlPreview) =>
          this.UrlPreview = trackUrlPreview;

        public void SetAlbumName(string albumName) =>
            this.Album = albumName;

        public void SetTrackDuration(double duration) =>
            this.Duration = TimeSpan.FromMilliseconds(duration);
    }
}
