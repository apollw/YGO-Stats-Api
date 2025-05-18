using YGO_Duel_Stats_Api.Models.Dtos.Simple;
using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Services.Interfaces
{
    public interface IDuelService
    {
        Task<IEnumerable<Duel>> GetAllAsync();
        Task<Duel?> GetByIdAsync(Guid id);
        Task<Duel> CreateAsync(CreateDuelDto dto);
        Task<Duel> UpdateAsync(Guid id, UpdateDuelDto dto);
        Task DeleteAsync(Guid id);
    }
}
