using YGO_Duel_Stats_Api.Models.Dtos.Simple;
using YGO_Duel_Stats_Api.Models;
using YGO_Duel_Stats_Api.Services.Interfaces;
using YGO_Duel_Stats_Api.Repositories.Interfaces;

namespace YGO_Duel_Stats_Api.Services.Implementations
{
    public class DuelService : IDuelService
    {
        private readonly IDuelRepository _repo;
        public DuelService(IDuelRepository repo) => _repo = repo;

        public async Task<IEnumerable<Duel>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Duel?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<Duel> CreateAsync(CreateDuelDto dto)
        {
            // Validate required fields
            if (dto.PlayerAId == Guid.Empty || dto.PlayerBId == Guid.Empty)
                throw new ArgumentException("PlayerAId and PlayerBId are required");
            if (dto.DeckAId == Guid.Empty || dto.DeckBId == Guid.Empty)
                throw new ArgumentException("DeckAId and DeckBId are required");

            // Set date: manual or now
            var duelDate = dto.DuelDate ?? DateTime.UtcNow;

            var duel = new Duel
            {
                PlayerAId = dto.PlayerAId,
                PlayerBId = dto.PlayerBId,
                DeckAId = dto.DeckAId,
                DeckBId = dto.DeckBId,
                WinnerId = dto.WinnerId,
                DuelDate = duelDate
            };
            return await _repo.CreateAsync(duel);
        }

        public async Task<Duel> UpdateAsync(Guid id, UpdateDuelDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Duel not found");

            // Update properties
            if (dto.PlayerAId != Guid.Empty) existing.PlayerAId = dto.PlayerAId;
            if (dto.PlayerBId != Guid.Empty) existing.PlayerBId = dto.PlayerBId;
            if (dto.DeckAId != Guid.Empty) existing.DeckAId = dto.DeckAId;
            if (dto.DeckBId != Guid.Empty) existing.DeckBId = dto.DeckBId;
            existing.WinnerId = dto.WinnerId;

            // Set date: manual or now
            existing.DuelDate = dto.DuelDate ?? DateTime.UtcNow;

            return await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Duel not found");

            await _repo.DeleteAsync(id);
        }
    }
}
