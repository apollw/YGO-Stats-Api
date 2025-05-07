using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using YGO_Duel_Stats_Api.Models;
using Microsoft.EntityFrameworkCore;
using YGO_Duel_Stats_Api.Data;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DuelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Duel>> Create(Duel duel)
        {
            _context.Duels.Add(duel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByDuelist), new { duelistId = duel.PlayerAId }, duel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Duel duel)
        {
            if (id != duel.Id) return BadRequest();
            _context.Entry(duel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Duels.FindAsync(id);
            if (item == null) return NotFound();
            _context.Duels.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("byDuelist/{duelistId}")]
        public async Task<ActionResult<IEnumerable<Duel>>> GetByDuelist(Guid duelistId)
        {
            var list = await _context.Duels
                .Where(d => d.PlayerAId == duelistId || d.PlayerBId == duelistId)
                .ToListAsync();
            return list;
        }

        [HttpGet("byDate/{day}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<Duel>>> GetByDate(int day, int month, int year)
        {
            var list = await _context.Duels
                .Where(d => d.DuelDate.Day == day &&
                            d.DuelDate.Month == month &&
                            d.DuelDate.Year == year)
                .ToListAsync();
            return list;
        }
    }
}
