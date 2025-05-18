using YGO_Duel_Stats_Api.Models;
using YGO_Duel_Stats_Api.Repositories.Interfaces;

namespace YGO_Duel_Stats_Api.Repositories.Implementations
{
    public class DeckRepository : IDeckRepository
    {
        private readonly Supabase.Client _supabase;

        public DeckRepository(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<Deck>> GetAllAsync()
        {
            var res = await _supabase.From<Deck>().Get();
            return res.Models;
        }

        public async Task<Deck?> GetByIdAsync(Guid id)
        {
            var res = await _supabase.From<Deck>()
                .Where(x => x.Id == id)
                .Get();
            return res.Models.FirstOrDefault();
        }

        public async Task<Deck> CreateAsync(Deck deck)
        {
            var res = await _supabase.From<Deck>().Insert(deck);
            return res.Models.First();
        }

        public async Task<Deck> UpdateAsync(Deck deck)
        {
            var res = await _supabase.From<Deck>()
                .Where(x => x.Id == deck.Id)
                .Update(deck);
            return res.Models.First();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Deck não encontrado");

            await _supabase.From<Deck>()
                .Where(x => x.Id == id)
                .Delete();
        }
    }
}
