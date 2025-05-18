namespace YGO_Duel_Stats_Api.Models.Dtos.General
{
    public record DuelDto(
        Guid Id,
        Guid PlayerAId,
        Guid PlayerBId,
        Guid DeckAId,
        Guid DeckBId,
        Guid? WinnerId,
        DateTime DuelDate
    );
}
