using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKZ_KPZ.Models
{
    public class PlaylistDTO
    {
 
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }

        public  List<SongsDTO> Songs { get; set; }
    }
}
