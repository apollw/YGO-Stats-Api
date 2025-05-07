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
    public class DeckController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeckController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Deck>> Create(Deck deck)
        {
            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = deck.Id }, deck);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deck>>> GetAll()
        {
            return await _context.Decks.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Deck deck)
        {
            if (id != deck.Id) return BadRequest();
            _context.Entry(deck).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Decks.FindAsync(id);
            if (item == null) return NotFound();
            _context.Decks.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
