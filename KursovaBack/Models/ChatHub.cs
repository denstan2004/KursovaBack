using System.Text.Json.Serialization;

namespace KursovaBack.Models
{
    public class ChatHub
    {
        public Guid Id { get; set; }

        public Guid User1 { get; set; }

        public Guid User2 { get; set; }

        [JsonIgnore]
        public List<Message>? Messages { get; set; }

        public string? LastMessage { get; set; }

        public DateTime? LastMessageDate { get; set; }

        public string? LastMessageSender { get; set; }
    }
}
