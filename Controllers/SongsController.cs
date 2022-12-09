using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EKZ_KPZ.Models;

namespace EKZ_KPZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly KPZ_EkzContext _context;

        public SongsController(KPZ_EkzContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongsDTO>>> GetSongs()
        {
           return (await _context.Songs.ToListAsync()).Select(s=>new SongsDTO
           {
               PlaylistId = s.PlaylistId,
               SongName = s.SongName,

           }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongsDTO>> GetSongs(int id)
        {
           
            using (var db = new KPZ_EkzContext())
            {
                var song = db.Songs.FirstOrDefault(s => s.SongId == id);
                if(song == null)
                {
                    return BadRequest();
                }
                var result = new SongsDTO
                {
                    PlaylistId = song.PlaylistId,
                    SongName = song.SongName
                };
                return result;
            }


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongs(int id,[FromBody] SongsDTO songs)
        {
            Songs updateSongs = new Songs
            {
                SongId = id,
                SongName = songs.SongName,
                PlaylistId = songs.PlaylistId
            };
            var song = _context.Songs.Update(updateSongs);
            _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public  IActionResult PostSongs([FromBody] SongsDTO songs)
        {
            //_context.Songs.Add(songs);
            Songs newSong = new Songs
            {
                SongName = songs.SongName,
                PlaylistId = songs.PlaylistId
            };
            var song = _context.Songs.Add(newSong);
             _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Songs>> DeleteSongs(int id)
        {
            var songs = await _context.Songs.FindAsync(id);
            if (songs == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(songs);
            await _context.SaveChangesAsync();

            return songs;
        }

        private bool SongsExists(int id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }
    }
}
