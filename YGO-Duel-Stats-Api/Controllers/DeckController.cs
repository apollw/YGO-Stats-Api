using Microsoft.AspNetCore.Mvc;
using YGO_Duel_Stats_Api.Models.Dtos;
using YGO_Duel_Stats_Api.Services;

namespace YGO_Duel_Stats_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private readonly IDeckService _deckService;
        public DecksController(IDeckService deckService) => _deckService = deckService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeckDto>>> GetAll()
        {
            var decks = await _deckService.GetAllAsync();
            var dtos = decks.Select(d => new DeckDto(d.Id, d.Name, d.RegistrationDate));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeckDto>> GetById(Guid id)
        {
            var deck = await _deckService.GetByIdAsync(id);
            if (deck is null) return NotFound();
            return Ok(new DeckDto(deck.Id, deck.Name, deck.RegistrationDate));
        }

        [HttpPost]
        public async Task<ActionResult<DeckDto>> Create(CreateDeckDto input)
        {
            try
            {
                var created = await _deckService.CreateAsync(input);
                return CreatedAtAction(nameof(GetById), new { id = created.Id },
                                       new DeckDto(created.Id, created.Name, created.RegistrationDate));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeckDto>> Update(Guid id, UpdateDeckDto input)
        {
            try
            {
                var updated = await _deckService.UpdateAsync(id, input);
                return Ok(new DeckDto(updated.Id, updated.Name, updated.RegistrationDate));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _deckService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }   
}
