namespace YGO_Duel_Stats_Api.Models.Dtos
{
    public record CreateDuelDto(
        Guid PlayerAId,
        Guid PlayerBId,
        Guid DeckAId,
        Guid DeckBId,
        Guid? WinnerId,
        DateTime? DuelDate
    );
}
