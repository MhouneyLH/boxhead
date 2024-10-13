using Supabase.Postgrest.Attributes;

namespace Boxhead.Domain.Models
{
    public record Round([property: Column("roundNumber")] int RoundNumber,
                        [property: Column("enemyCount")] int EnemyCount,
                        [property: Column("enemyConfiguration")] EnemyConfiguration EnemyConfiguration,
                        [property: Column("player")] Player Player)
    {
        public static Round CreateFirstRound() => new(1, 5, new EnemyConfiguration(30.0f, 10.0f, 0.5f), new Player(100.0f, 10.0f, 1.0f));
    }

    public record EnemyConfiguration([property: Column("health")] float Health,
                                     [property: Column("damage")] float Damage,
                                     [property: Column("speed")] float Speed)
    {
        public bool IsAlive() => Health > 0.0f;
    }

    public record Player([property: Column("health")] float Health,
                         [property: Column("damage")] float Damage,
                         [property: Column("speed")] float Speed)
    {
        public bool IsAlive() => Health > 0.0f;
    }
}