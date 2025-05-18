using YGO_Duel_Stats_Api.Models;
using YGO_Duel_Stats_Api.Repositories.Interfaces;

namespace YGO_Duel_Stats_Api.Repositories.Implementations
{
    public class DuelRepository : IDuelRepository
    {
        private readonly Supabase.Client _supabase;

        public DuelRepository(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<Duel>> GetAllAsync()
        {
            var res = await _supabase.From<Duel>().Get();
            return res.Models;
        }

        public async Task<Duel?> GetByIdAsync(Guid id)
        {
            var res = await _supabase.From<Duel>()
                .Where(x => x.Id == id)
                .Get();
            return res.Models.FirstOrDefault();
        }

        public async Task<Duel> CreateAsync(Duel duel)
        {
            var res = await _supabase.From<Duel>().Insert(duel);
            return res.Models.First();
        }

        public async Task<Duel> UpdateAsync(Duel duel)
        {
            var res = await _supabase.From<Duel>()
                .Where(x => x.Id == duel.Id)
                .Update(duel);
            return res.Models.First();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Duel not found");

            await _supabase.From<Duel>()
                .Where(x => x.Id == id)
                .Delete();
        }
    }
}
