using YGO_Duel_Stats_Api.Interfaces;
using YGO_Duel_Stats_Api.Models.Dtos;
using YGO_Duel_Stats_Api.Models;

namespace YGO_Duel_Stats_Api.Services.Implementations
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _repo;
        public DeckService(IDeckRepository repo) => _repo = repo;

        public async Task<Deck> CreateAsync(CreateDeckDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Campo Nome é obrigatório");

            // Regra: não inserir dois decks com o mesmo nome
            var all = await _repo.GetAllAsync();
            if (all.Any(d => d.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Deck de nome '{dto.Name}' já existe");

            var model = new Deck { Name = dto.Name };
            return await _repo.CreateAsync(model);
        }

        public async Task<IEnumerable<Deck>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Deck?> GetByIdAsync(Guid id)
            => await _repo.GetByIdAsync(id);

        public async Task<Deck> UpdateAsync(Guid id, UpdateDeckDto dto)
        {
            var name = dto.Name;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Campo Nome é obrigatório", nameof(dto));

            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Deck não encontrado");

            var all = await _repo.GetAllAsync();
            if (all.Any(d => d.Id != id && d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Deck de nome '{name}' já existe");

            existing.Name = name;
            return await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Deck não encontrado");

            await _repo.DeleteAsync(id);
        }

    }
}
