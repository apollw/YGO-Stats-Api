using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using YGO_Duel_Stats_Api.Interfaces;
using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private readonly IDeckRepository _repo;
        public DecksController(IDeckRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deck>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Deck>> GetById(Guid id)
        {
            var deck = await _repo.GetByIdAsync(id);
            return deck is null ? NotFound() : Ok(deck);
        }

        [HttpPost]
        public async Task<ActionResult<Deck>> Create(Deck deck)
        {
            var created = await _repo.CreateAsync(deck);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Deck>> Update(Guid id, Deck deck)
        {
            if (deck.Id != id) return BadRequest();
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();
            var updated = await _repo.UpdateAsync(deck);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
