using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YGO_Duel_Stats_Api.Interfaces;
using YGO_Duel_Stats_Api.Models;
using YGO_Duel_Stats_Api.Models.Dtos;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelistsController : ControllerBase
    {
        private readonly IDuelistRepository _repo;
        public DuelistsController(IDuelistRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuelistDto>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            var dtos = list.Select(d => new DuelistDto(d.Id, d.Name, d.AvatarUrl));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuelistDto>> GetById(Guid id)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d is null) return NotFound();
            return Ok(new DuelistDto(d.Id, d.Name, d.AvatarUrl));
        }

        [HttpPost]
        public async Task<ActionResult<DuelistDto>> Create(CreateDuelistDto input)
        {
            var model = new Duelist { Name = input.Name, AvatarUrl = input.AvatarUrl };
            var created = await _repo.CreateAsync(model);
            var dto = new DuelistDto(created.Id, created.Name, created.AvatarUrl);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DuelistDto>> Update(Guid id, CreateDuelistDto input)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();
            existing.Name = input.Name;
            existing.AvatarUrl = input.AvatarUrl;
            var updated = await _repo.UpdateAsync(existing);
            return Ok(new DuelistDto(updated.Id, updated.Name, updated.AvatarUrl));
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
