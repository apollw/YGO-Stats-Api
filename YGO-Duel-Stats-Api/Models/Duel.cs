namespace YGO_Duel_Stats_Api.Models
{
    public class Duel
    {
        public Guid Id { get; set; }
        public Guid PlayerAId { get; set; }
        public Guid PlayerBId { get; set; }
        public Guid DeckAId { get; set; }
        public Guid DeckBId { get; set; }
        public Guid? WinnerId { get; set; }
        public DateTime DuelDate { get; set; } = DateTime.UtcNow;

        //// Navigation
        //public Duelist? PlayerA { get; set; }
        //public Duelist? PlayerB { get; set; }
        //public Deck? DeckA { get; set; }
        //public Deck? DeckB { get; set; }
        //public Duelist? Winner { get; set; }
    }
}
