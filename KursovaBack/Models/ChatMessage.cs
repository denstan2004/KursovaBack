namespace project_back.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
