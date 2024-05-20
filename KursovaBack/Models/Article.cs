namespace KursovaBack.Models
{
    public class Article
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Content { get; set; }

        public string? Author { get; set; }

        public string? Photo { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
