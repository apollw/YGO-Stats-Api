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
    public class DuelController : ControllerBase
    {
        private readonly IDuelRepository _repo;
        public DuelController(IDuelRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuelDto>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            var dtos = list.Select(d => new DuelDto(
                d.Id, d.PlayerAId, d.PlayerBId, d.DeckAId, d.DeckBId, d.WinnerId, d.DuelDate));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuelDto>> GetById(Guid id)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d is null) return NotFound();
            return Ok(new DuelDto(
                d.Id, d.PlayerAId, d.PlayerBId, d.DeckAId, d.DeckBId, d.WinnerId, d.DuelDate));
        }

        [HttpGet("byDuelist/{duelistId}")]
        public async Task<ActionResult<IEnumerable<DuelDto>>> GetByDuelist(Guid duelistId)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all
                .Where(d => d.PlayerAId == duelistId || d.PlayerBId == duelistId)
                .Select(d => new DuelDto(
                    d.Id, d.PlayerAId, d.PlayerBId, d.DeckAId, d.DeckBId, d.WinnerId, d.DuelDate));
            return Ok(filtered);
        }

        [HttpGet("byDate/{day}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<DuelDto>>> GetByDate(int day, int month, int year)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all
                .Where(d => d.DuelDate.Day == day && d.DuelDate.Month == month && d.DuelDate.Year == year)
                .Select(d => new DuelDto(
                    d.Id, d.PlayerAId, d.PlayerBId, d.DeckAId, d.DeckBId, d.WinnerId, d.DuelDate));
            return Ok(filtered);
        }

        [HttpPost]
        public async Task<ActionResult<DuelDto>> Create(CreateDuelDto input)
        {
            var model = new Duel
            {
                PlayerAId = input.PlayerAId,
                PlayerBId = input.PlayerBId,
                DeckAId = input.DeckAId,
                DeckBId = input.DeckBId,
                WinnerId = input.WinnerId,
                DuelDate = input.DuelDate ?? DateTime.UtcNow
            };
            var created = await _repo.CreateAsync(model);
            var dto = new DuelDto(
                created.Id, created.PlayerAId, created.PlayerBId,
                created.DeckAId, created.DeckBId, created.WinnerId, created.DuelDate);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DuelDto>> Update(Guid id, CreateDuelDto input)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();

            existing.PlayerAId = input.PlayerAId;
            existing.PlayerBId = input.PlayerBId;
            existing.DeckAId = input.DeckAId;
            existing.DeckBId = input.DeckBId;
            existing.WinnerId = input.WinnerId;
            existing.DuelDate = input.DuelDate ?? existing.DuelDate;

            var updated = await _repo.UpdateAsync(existing);
            var dto = new DuelDto(
                updated.Id, updated.PlayerAId, updated.PlayerBId,
                updated.DeckAId, updated.DeckBId, updated.WinnerId, updated.DuelDate);
            return Ok(dto);
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
