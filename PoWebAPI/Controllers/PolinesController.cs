using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoWebAPI.Data;
using PoWebAPI.Models;

namespace PoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolinesController : ControllerBase
    {
        private readonly PoContext _context;

        public PolinesController(PoContext context)
        {
            _context = context;
        }

        // GET: api/Polines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poline>>> GetPolines()
        {
            return await _context.Polines.ToListAsync();
        }

        // GET: api/Polines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Poline>> GetPoline(int id)
        {
            var poline = await _context.Polines.FindAsync(id);

            if (poline == null)
            {
                return NotFound();
            }

            return poline;
        }

        // PUT: api/Polines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoline(int id, Poline poline)
        {
            if (id != poline.Id)
            {
                return BadRequest();
            }

            _context.Entry(poline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolineExists(id))
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

        // POST: api/Polines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Poline>> PostPoline(Poline poline)
        {
            _context.Polines.Add(poline);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoline", new { id = poline.Id }, poline);
        }

        // DELETE: api/Polines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Poline>> DeletePoline(int id)
        {
            var poline = await _context.Polines.FindAsync(id);
            if (poline == null)
            {
                return NotFound();
            }

            _context.Polines.Remove(poline);
            await _context.SaveChangesAsync();

            return poline;
        }

        private bool PolineExists(int id)
        {
            return _context.Polines.Any(e => e.Id == id);
        }
    }
}
