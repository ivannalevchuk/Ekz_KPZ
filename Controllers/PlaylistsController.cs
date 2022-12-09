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
    public class PlaylistsController : ControllerBase
    {
        private readonly KPZ_EkzContext _context;

        public PlaylistsController(KPZ_EkzContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistDTO>>> GetPlaylists()
        {
            return (await _context.Playlists.Include(p => p.Songs).ToListAsync()).Select(p => new PlaylistDTO
            {
                PlaylistId = p.PlaylistId,
                PlaylistName = p.PlaylistName,
                Songs = p.Songs?.Select(s => new SongsDTO
                {
                    PlaylistId = s.PlaylistId,
                    SongName = s.SongName,

                }).ToList(),


            }).ToList();
        }

  
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDTO>> GetPlaylists([FromRoute]int id)
        {
            using (var db = new KPZ_EkzContext())
            {
                var playlist = db.Playlists.Include(s=>s.Songs).FirstOrDefault(s => s.PlaylistId == id);
                if (playlist == null)
                {
                    return BadRequest();
                }
                var result = new PlaylistDTO
                {
                    PlaylistId = playlist.PlaylistId,
                    PlaylistName = playlist.PlaylistName,
                    Songs = playlist.Songs?.Select(s => new SongsDTO
                    {
                        PlaylistId = s.PlaylistId,
                        SongName = s.SongName,

                    }).ToList(),
                };
                return result;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylists(int id, Playlists playlists)
        {
            if (id != playlists.PlaylistId)
            {
                return BadRequest();
            }

            _context.Entry(playlists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Playlists>> PostPlaylists(Playlists playlists)
        {
            _context.Playlists.Add(playlists);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaylists", new { id = playlists.PlaylistId }, playlists);
        }
        
        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Playlists>> DeletePlaylists(int id)
        {
            var playlists = await _context.Playlists.FindAsync(id);
            if (playlists == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlists);
            await _context.SaveChangesAsync();

            return playlists;
        }

        private bool PlaylistsExists(int id)
        {
            return _context.Playlists.Any(e => e.PlaylistId == id);
        }
    }
}
