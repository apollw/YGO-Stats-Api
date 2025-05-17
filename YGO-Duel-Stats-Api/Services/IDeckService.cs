using YGO_Duel_Stats_Api.Models.Dtos;
using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Services
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
