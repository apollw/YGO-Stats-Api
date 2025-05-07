namespace YGO_Duel_Stats_Api.Models
{
    public class Duelist
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? AvatarUrl { get; set; }

        //// Navigation
        //public ICollection<Deck>? Decks { get; set; }
        //public ICollection<Duel>? DuelsAsPlayerA { get; set; }
        //public ICollection<Duel>? DuelsAsPlayerB { get; set; }
        //public ICollection<Duel>? DuelsWon { get; set; }
    }
}
