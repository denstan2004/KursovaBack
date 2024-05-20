using KursovaBack.Models.Enums;
using System.Text.Json.Serialization;

namespace KursovaBack.Models
{
    public class Message
    {
        public Guid Id { get; set; }

        public Guid SenderId { get; set; }

        [JsonIgnore]
        public User Sender { get; set; }

        public Guid ChatHubId { get; set; }

        [JsonIgnore]
        public ChatHub ChatHub { get; set; }

        public string Content { get; set; }

        public byte[]? Picture { get; set; }

        public DateTime SendDate { get; set; }

        public ChatMessageStatus Status { get; set; } = 0;

    }
}
