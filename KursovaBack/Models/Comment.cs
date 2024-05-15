namespace KursovaBack.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; }
        public Guid PostId { get; set; }

        public Comment(Guid id, Guid authorId, string text,Guid postId)
        {
            Id = id;
            AuthorId = authorId;
            Text = text;
            PostId = postId;
        }

        public Comment()
        {
        }
    }
}
