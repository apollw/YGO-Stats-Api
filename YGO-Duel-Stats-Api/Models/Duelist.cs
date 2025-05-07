using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace YGO_Duel_Stats_Api.Models
{
    [Table("duelists")]
    public class Duelist : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("avatar_url")]
        public string? AvatarUrl { get; set; }
    }
}
