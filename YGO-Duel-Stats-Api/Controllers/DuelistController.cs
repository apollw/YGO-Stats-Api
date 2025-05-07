using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using YGO_Duel_Stats_Api.Data;
using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DuelistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Duelist>> Create(Duelist duelist)
        {
            _context.Duelists.Add(duelist);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = duelist.Id }, duelist);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Duelist>>> GetAll()
        {
            return await _context.Duelists.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Duelist duelist)
        {
            if (id != duelist.Id) return BadRequest();
            _context.Entry(duelist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Duelists.FindAsync(id);
            if (item == null) return NotFound();
            _context.Duelists.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
