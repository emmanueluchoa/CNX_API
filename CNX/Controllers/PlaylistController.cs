using CNX_Domain.Interfaces.Application;
using CNX_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

namespace CNX_API.Controllers
{
    [Route("api/Playlist")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistApplication _playlistApplication;
        private readonly ILogger<PlaylistController> _logger;

        public PlaylistController(IPlaylistApplication playlistApplication, ILogger<PlaylistController> logger)
        {
            this._playlistApplication = playlistApplication;
            this._logger = logger;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetPlaylist()
        {
            try
            {
                Claim locality = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Locality));
                PlaylistVM playlist = this._playlistApplication.GetPlaylistByWeatherLocalityCondition(locality.Value);

                return Ok(playlist);
            }
            catch (Exception error)
            {
                this._logger.LogError(error, error.Message);
                return BadRequest(error.Message);
            }
        }
    }
}