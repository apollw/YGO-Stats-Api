using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace YGO_Duel_Stats_Api.Models
{
    [Table("duels")]
    public class Duel : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("player_a_id")]
        public Guid PlayerAId { get; set; }

        [Column("player_b_id")]
        public Guid PlayerBId { get; set; }

        [Column("deck_a_id")]
        public Guid DeckAId { get; set; }

        [Column("deck_b_id")]
        public Guid DeckBId { get; set; }

        [Column("winner_id")]
        public Guid? WinnerId { get; set; }

        [Column("duel_date")]
        public DateTime DuelDate { get; set; }
    }
}
