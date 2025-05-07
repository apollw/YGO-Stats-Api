using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using YGO_Duel_Stats_Api.Interfaces;
using YGO_Duel_Stats_Api.Models;
using YGO_Duel_Stats_Api.Models.Dtos;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private readonly IDeckRepository _repo;
        public DecksController(IDeckRepository repo) => _repo = repo;

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Deck>>> GetAll()
        //{
        //    var list = await _repo.GetAllAsync();
        //    return Ok(list);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Deck>> GetById(Guid id)
        //{
        //    var deck = await _repo.GetByIdAsync(id);
        //    return deck is null ? NotFound() : Ok(deck);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Deck>> Create(Deck deck)
        //{
        //    var created = await _repo.CreateAsync(deck);
        //    return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Deck>> Update(Guid id, Deck deck)
        //{
        //    if (deck.Id != id) return BadRequest();
        //    var existing = await _repo.GetByIdAsync(id);
        //    if (existing is null) return NotFound();
        //    var updated = await _repo.UpdateAsync(deck);
        //    return Ok(updated);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var existing = await _repo.GetByIdAsync(id);
        //    if (existing is null) return NotFound();
        //    await _repo.DeleteAsync(id);
        //    return NoContent();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeckDto>>> GetAll()
        {
            var decks = await _repo.GetAllAsync();
            var dtos = decks.Select(d => new DeckDto(d.Id, d.Name));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeckDto>> GetById(Guid id)
        {
            var deck = await _repo.GetByIdAsync(id);
            if (deck is null) return NotFound();
            return Ok(new DeckDto(deck.Id, deck.Name));
        }

        [HttpPost]
        public async Task<ActionResult<DeckDto>> Create(CreateDeckDto input)
        {
            var model = new Deck { Name = input.Name };
            var created = await _repo.CreateAsync(model);
            var dto = new DeckDto(created.Id, created.Name);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeckDto>> Update(Guid id, CreateDeckDto input)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();

            existing.Name = input.Name;
            var updated = await _repo.UpdateAsync(existing);
            return Ok(new DeckDto(updated.Id, updated.Name));
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
