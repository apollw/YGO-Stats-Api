using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YGO_Duel_Stats_Api.Models.Dtos.General;
using YGO_Duel_Stats_Api.Models.Dtos.Simple;
using YGO_Duel_Stats_Api.Services.Interfaces;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelController : ControllerBase
    {
        private readonly IDuelService _service;
        public DuelController(IDuelService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuelDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            var dtos = list.Select(d => new DuelDto(
                d.Id, d.PlayerAId, d.PlayerBId, d.DeckAId, d.DeckBId, d.WinnerId, d.DuelDate));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuelDto>> GetById(Guid id)
        {
            var d = await _service.GetByIdAsync(id);
            if (d is null) return NotFound();
            return Ok(new DuelDto(
                d.Id, d.PlayerAId, d.PlayerBId, d.DeckAId, d.DeckBId, d.WinnerId, d.DuelDate));
        }

        [HttpGet("byDuelist/{duelistId}")]
        public async Task<ActionResult<IEnumerable<DuelDto>>> GetByDuelist(Guid duelistId)
        {
            var all = await _service.GetAllAsync();
            var filtered = all
                .Where(d => d.PlayerAId == duelistId || d.PlayerBId == duelistId)
                .Select(d => new DuelDto(
                    d.Id, d.PlayerAId, d.PlayerBId, d.DeckAId, d.DeckBId, d.WinnerId, d.DuelDate));
            return Ok(filtered);
        }

        [HttpGet("byDate/{day}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<DuelDto>>> GetByDate(int day, int month, int year)
        {
            var all = await _service.GetAllAsync();
            var filtered = all
                .Where(d => d.DuelDate.Day == day && d.DuelDate.Month == month && d.DuelDate.Year == year)
                .Select(d => new DuelDto(
                    d.Id, d.PlayerAId, d.PlayerBId, d.DeckAId, d.DeckBId, d.WinnerId, d.DuelDate));
            return Ok(filtered);
        }

        [HttpPost]
        public async Task<ActionResult<DuelDto>> Create(CreateDuelDto input)
        {
            try
            {
                var created = await _service.CreateAsync(input);
                var dto = new DuelDto(
                    created.Id, created.PlayerAId, created.PlayerBId,
                    created.DeckAId, created.DeckBId, created.WinnerId, created.DuelDate);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DuelDto>> Update(Guid id, UpdateDuelDto input)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, input);
                var dto = new DuelDto(
                    updated.Id, updated.PlayerAId, updated.PlayerBId,
                    updated.DeckAId, updated.DeckBId, updated.WinnerId, updated.DuelDate);
                return Ok(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
