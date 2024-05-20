namespace KursovaBack.Models
{
    public class VideoChatHub
    {
        public Guid Id { get; set; }

        public Guid User1Id { get; set; }

        public Guid User2Id { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Duration { get; set; }
    }
}
