using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EKZ_KPZ.Models
{
    public partial class Playlists
    {
        public Playlists()
        {
            Songs = new HashSet<Songs>();
        }

        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }

        public virtual ICollection<Songs> Songs { get; set; }
    }
}
