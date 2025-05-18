using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using Supabase.Realtime;
using Supabase.Storage;
using YGO_Duel_Stats_Api.Models;
using YGO_Duel_Stats_Api.Repositories.Interfaces;

namespace YGO_Duel_Stats_Api.Repositories.Implementations
{
    public class DuelistRepository : IDuelistRepository
    {
        private readonly Supabase.Client _supabase;

        public DuelistRepository(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<Duelist>> GetAllAsync()
        {
            var res = await _supabase.From<Duelist>().Get();
            return res.Models;
        }

        public async Task<Duelist> CreateAsync(Duelist duelist)
        {
            var res = await _supabase.From<Duelist>().Insert(duelist);
            return res.Models[0];
        }

        public async Task<Duelist?> GetByIdAsync(Guid id)
        {
            var res = await _supabase
                .From<Duelist>()
                .Where(x => x.Id == id)
                .Get();
            return res.Models.FirstOrDefault();
        }

        public async Task<Duelist> UpdateAsync(Duelist duelist)
        {
            var res = await _supabase
                .From<Duelist>()
                .Where(x => x.Id == duelist.Id)
                .Update(duelist);
            return res.Models.First();
        }

        public async Task DeleteAsync(Guid id)
        {
            // verifica existência
            var existing = await GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Duelist not found");

            await _supabase
                .From<Duelist>()
                .Where(x => x.Id == id)
                .Delete();
        }
    }
}
