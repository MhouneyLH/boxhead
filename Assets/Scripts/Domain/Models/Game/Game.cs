using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Boxhead.Domain.Models
{
    /// <summary>
    /// Represents a game.
    /// </summary>
    [Table("games")]
    public class Game : BaseModel
    {
        public Game() { }

        public Game(Guid id, DateTime createdAt, GameData data)
        {
            Id = id;
            CreatedAt = createdAt;
            Data = data;
        }

        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("version")]
        public int Version { get; set; } = 1;

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("data")]
        public GameData Data { get; set; }
    }
}
