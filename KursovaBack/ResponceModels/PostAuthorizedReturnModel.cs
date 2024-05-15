namespace KursovaBack.ResponceModels
{
    public class PostAuthorizedReturnModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid ProjectId { get; set; }
        public Guid AuthorId { get; set; }
        public int Likes { get; set; }
        public byte[] Image { get; set; }
        public DateTime Date { get; set; }
        public bool IsLiked { get; set; }

        public PostAuthorizedReturnModel(Guid id, string title, string text, Guid projectId, Guid authorId, int likes, byte[] image, DateTime date, bool isLiked)
        {
            Id = id;
            Title = title;
            Text = text;
            ProjectId = projectId;
            AuthorId = authorId;
            Likes = likes;
            Image = image;
            Date = date;
            IsLiked = isLiked;
        }

        public PostAuthorizedReturnModel()
        {
        }
    }
}
