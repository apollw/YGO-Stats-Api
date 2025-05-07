namespace YGO_Duel_Stats_Api.Models
{
    public class Deck
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        //// Navigation
        //public Duelist? Duelist { get; set; }
        //public ICollection<Duel>? DuelsAsDeckA { get; set; }
        //public ICollection<Duel>? DuelsAsDeckB { get; set; }
    }

}
