using CNX_Domain.Entities.Enums;
using CNX_Domain.Models;

namespace CNX_Domain.Interfaces.Services
{
    public interface IMusicApi
    {
        PlaylistVM GetPlayList(EnumPlayLists playLists);
    }
}
