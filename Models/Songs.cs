using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EKZ_KPZ.Models
{
    public partial class Songs
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public int? PlaylistId { get; set; }

        public virtual Playlists Playlist { get; set; }
    }
}
