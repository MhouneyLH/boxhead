using Supabase.Postgrest.Attributes;

namespace Boxhead.Domain.Models
{
    /// <summary>
    /// Represents the data of a game.
    /// </summary> 
    public record GameData([property: Column("score")] int Score,
                           [property: Column("round")] int Round);
}
