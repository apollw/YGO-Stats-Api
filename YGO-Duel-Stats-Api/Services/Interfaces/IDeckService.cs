using YGO_Duel_Stats_Api.Models;
using YGO_Duel_Stats_Api.Models.Dtos.Simple;

namespace YGO_Duel_Stats_Api.Services.Interfaces
{
    public interface IDeckService
    {
        Task<IEnumerable<Deck>> GetAllAsync();
        Task<Deck?> GetByIdAsync(Guid id);
        Task<Deck> CreateAsync(CreateDeckDto dto);
        Task<Deck> UpdateAsync(Guid id, UpdateDeckDto dto);
        Task DeleteAsync(Guid id);
    }
}
