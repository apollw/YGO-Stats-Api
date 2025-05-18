using YGO_Duel_Stats_Api.Models.Dtos.Simple;
using YGO_Duel_Stats_Api.Models;
using YGO_Duel_Stats_Api.Repositories.Interfaces;

namespace YGO_Duel_Stats_Api.Services.Implementations
{
    public class DuelistService
    {
        private readonly IDuelistRepository _repo;
        public DuelistService(IDuelistRepository repo) => _repo = repo;

        public async Task<IEnumerable<Duelist>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Duelist?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<Duelist> CreateAsync(CreateDuelistDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name is required", nameof(dto.Name));

            // Rule: Unique Name
            var all = await _repo.GetAllAsync();
            if (all.Any(d => d.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Duelist '{dto.Name}' already exists.");

            var model = new Duelist
            {
                Name = dto.Name,
                AvatarUrl = dto.AvatarUrl
            };
            return await _repo.CreateAsync(model);
        }

        public async Task<Duelist> UpdateAsync(Guid id, UpdateDuelistDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name is required", nameof(dto.Name));

            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Duelist not found");

            // Unique name among others
            var all = await _repo.GetAllAsync();
            if (all.Any(d => d.Id != id && d.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Duelist '{dto.Name}' already exists.");

            existing.Name = dto.Name;
            existing.AvatarUrl = dto.AvatarUrl;
            return await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Duelist not found");

            await _repo.DeleteAsync(id);
        }
    }
}
