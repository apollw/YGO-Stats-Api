using Microsoft.AspNetCore.Mvc;
using YGO_Duel_Stats_Api.Models.Dtos.General;
using YGO_Duel_Stats_Api.Models.Dtos.Simple;
using YGO_Duel_Stats_Api.Services.Interfaces;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuelistsController : ControllerBase
    {
        private readonly IDuelistService _duelistService;
        public DuelistsController(IDuelistService service) => _duelistService = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuelistDto>>> GetAll()
        {
            var list = await _duelistService.GetAllAsync();
            var dtos = list.Select(d => new DuelistDto(d.Id, d.Name, d.AvatarUrl));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuelistDto>> GetById(Guid id)
        {
            var d = await _duelistService.GetByIdAsync(id);
            if (d is null) return NotFound();
            return Ok(new DuelistDto(d.Id, d.Name, d.AvatarUrl));
        }

        [HttpPost]
        public async Task<ActionResult<DuelistDto>> Create(CreateDuelistDto input)
        {
            try
            {
                var created = await _duelistService.CreateAsync(input);
                var dto = new DuelistDto(created.Id, created.Name, created.AvatarUrl);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DuelistDto>> Update(Guid id, UpdateDuelistDto input)
        {
            try
            {
                var updated = await _duelistService.UpdateAsync(id, input);
                return Ok(new DuelistDto(updated.Id, updated.Name, updated.AvatarUrl));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _duelistService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
