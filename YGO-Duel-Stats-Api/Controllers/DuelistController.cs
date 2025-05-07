using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YGO_Duel_Stats_Api.Interfaces;
using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelistsController : ControllerBase
    {
        private readonly IDuelistRepository _repo;
        public DuelistsController(IDuelistRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Duelist>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Duelist>> GetById(Guid id)
        {
            var duelist = await _repo.GetByIdAsync(id);
            return duelist is null ? NotFound() : Ok(duelist);
        }

        [HttpPost]
        public async Task<ActionResult<Duelist>> Create(Duelist duelist)
        {
            var created = await _repo.CreateAsync(duelist);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Duelist>> Update(Guid id, Duelist duelist)
        {
            if (duelist.Id != id) return BadRequest();
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();
            var updated = await _repo.UpdateAsync(duelist);
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
