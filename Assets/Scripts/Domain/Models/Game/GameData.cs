using System;
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
                           [property: Column("round")] Round Round)
    {
        private const float ENEMY_COUNT_INCREMENT_FACTOR = 1.1f;
        private const float ENEMY_HEALTH_INCREMENT_FACTOR = 1.05f;
        private const float ENEMY_SPEED_INCREMENT_FACTOR = 1.01f;

        public static GameData CreateNew() => new(0, Round.CreateFirstRound());

        public GameData NextRound() => this with
        {
            Round = Round with
            {
                RoundNumber = Round.RoundNumber + 1,
                EnemyCount = (int)MathF.Ceiling(Round.EnemyCount * ENEMY_COUNT_INCREMENT_FACTOR),
                EnemyConfiguration = Round.EnemyConfiguration with
                {
                    Health = Round.EnemyConfiguration.Health * ENEMY_HEALTH_INCREMENT_FACTOR,
                    Speed = Round.EnemyConfiguration.Speed * ENEMY_SPEED_INCREMENT_FACTOR
                }
            }
        };
    }
}
