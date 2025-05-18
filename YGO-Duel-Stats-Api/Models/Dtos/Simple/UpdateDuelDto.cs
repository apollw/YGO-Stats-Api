namespace YGO_Duel_Stats_Api.Models.Dtos.Simple
{
    public record UpdateDuelDto(
            Guid PlayerAId,
            Guid PlayerBId,
            Guid DeckAId,
            Guid DeckBId,
            Guid? WinnerId,
            DateTime? DuelDate
        );
}
