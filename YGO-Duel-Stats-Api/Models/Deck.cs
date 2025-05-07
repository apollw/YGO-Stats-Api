using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace YGO_Duel_Stats_Api.Models
{
    [Table("decks")]
    public class Deck : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
