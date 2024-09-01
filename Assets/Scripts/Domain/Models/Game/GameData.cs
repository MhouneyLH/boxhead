using Supabase.Postgrest.Attributes;

namespace Boxhead.Domain.Models
{
    /// <summary>
    /// Represents the data of a game.
    /// This is everything that really matters for the game.
    /// The created date or version e. g. are not part of the game data.
    /// These are stored in the Game class. <see cref="Game"/>.
    /// </summary> 
    public record GameData([property: Column("score")] int Score,
                           [property: Column("round")] int Round);
}
