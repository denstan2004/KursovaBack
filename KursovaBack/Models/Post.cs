namespace KursovaBack.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid ProjectId { get; set; }
        public Guid AuthorId { get; set; }
        public int Likes { get; set; }
        public IFormFile fromFile { get; set; }
        public string ImageBase64 { get; set; }
        public byte[] Image { get; set; }
        public DateTime Date { get; set; }

        public Post(Guid id, string title, string text, Guid projectId, Guid authorId, int likes, byte[] image, DateTime date, IFormFile fromFile)
        {
            Id = id;
            Title = title;
            Text = text;
            ProjectId = projectId;
            AuthorId = authorId;
            Likes = likes;
            Image = image;
            Date = date;
            this.fromFile = fromFile;
        }

        public Post()
        {
        }
    }
}
 