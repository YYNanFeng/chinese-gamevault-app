using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gamevault.Models
{
    public enum State
    {
        [Description("未玩")]
        UNPLAYED,
        [Description("无限")]
        INFINITE,
        [Description("游玩中")]
        PLAYING,
        [Description("已完成")]
        COMPLETED,
        [Description("暂时搁置")]
        ABORTED_TEMPORARY,
        [Description("永久放弃")]
        ABORTED_PERMANENT
    }
    public class Progress
    {
        [JsonPropertyName("id")]
        public int? ID { get; set; }
        [JsonPropertyName("minutes_played")]
        public int? MinutesPlayed { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }
        [JsonPropertyName("last_played_at")]
        public DateTime? LastPlayedAt { get; set; }
        [JsonPropertyName("game")]
        public Game? Game { get; set; }
        [JsonPropertyName("user")]
        public User? User { get; set; }

    }

}
