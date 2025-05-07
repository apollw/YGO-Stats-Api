using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Interfaces
{
    public interface IDeckRepository
    {
        Task<IEnumerable<Deck>> GetAllAsync();
        Task<Deck?> GetByIdAsync(Guid id);
        Task<Deck> CreateAsync(Deck deck);
        Task<Deck> UpdateAsync(Deck deck);
        Task DeleteAsync(Guid id);
    }
}
