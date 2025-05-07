using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using YGO_Duel_Stats_Api.Interfaces;
using YGO_Duel_Stats_Api.Models;


namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelController : ControllerBase
    {
        private readonly IDuelRepository _repo;
        public DuelController(IDuelRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Duel>>> GetAll()
        {
            var all = await _repo.GetAllAsync();
            return Ok(all);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Duel>> GetById(Guid id)
        {
            var duel = await _repo.GetByIdAsync(id);
            return duel is null ? NotFound() : Ok(duel);
        }

        [HttpGet("byDuelist/{duelistId}")]
        public async Task<ActionResult<IEnumerable<Duel>>> GetByDuelist(Guid duelistId)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all.Where(d => d.PlayerAId == duelistId || d.PlayerBId == duelistId);
            return Ok(filtered);
        }

        [HttpGet("byDate/{day}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<Duel>>> GetByDate(int day, int month, int year)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all.Where(d => d.DuelDate.Day == day &&
                                          d.DuelDate.Month == month &&
                                          d.DuelDate.Year == year);
            return Ok(filtered);
        }

        [HttpPost]
        public async Task<ActionResult<Duel>> Create(Duel duel)
        {
            var created = await _repo.CreateAsync(duel);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Duel>> Update(Guid id, Duel duel)
        {
            if (duel.Id != id) return BadRequest();
            var exists = await _repo.GetByIdAsync(id);
            if (exists is null) return NotFound();
            var updated = await _repo.UpdateAsync(duel);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exists = await _repo.GetByIdAsync(id);
            if (exists is null) return NotFound();
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
