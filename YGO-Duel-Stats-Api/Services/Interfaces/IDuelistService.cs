using YGO_Duel_Stats_Api.Models.Dtos.Simple;
using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Services.Interfaces
{
    public interface IDuelistService
    {
        Task<IEnumerable<Duelist>> GetAllAsync();
        Task<Duelist?> GetByIdAsync(Guid id);
        Task<Duelist> CreateAsync(CreateDuelistDto dto);
        Task<Duelist> UpdateAsync(Guid id, UpdateDuelistDto dto);
        Task DeleteAsync(Guid id);
    }
}
