using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Repositories.Interfaces
{
    public interface IDuelRepository
    {
        Task<IEnumerable<Duel>> GetAllAsync();
        Task<Duel?> GetByIdAsync(Guid id);
        Task<Duel> CreateAsync(Duel duel);
        Task<Duel> UpdateAsync(Duel duel);
        Task DeleteAsync(Guid id);
    }

}
