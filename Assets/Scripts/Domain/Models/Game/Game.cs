using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Boxhead.Domain.Models
{
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

        public static Game CreateNew() => new(Guid.NewGuid(), DateTime.UtcNow, new GameData(0, 0));

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
