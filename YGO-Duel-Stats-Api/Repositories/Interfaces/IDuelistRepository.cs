using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Repositories.Interfaces
{
    public interface IDuelistRepository
    {
        Task<IEnumerable<Duelist>> GetAllAsync();
        Task<Duelist?> GetByIdAsync(Guid id);
        Task<Duelist> CreateAsync(Duelist duelist);
        Task<Duelist> UpdateAsync(Duelist duelist);
        Task DeleteAsync(Guid id);
    }
}
