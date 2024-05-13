namespace KursovaBack.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }

        public Task(Guid id, Guid projectId, Guid authorId, Guid userId, DateTime date, DateTime expiredDate, string message, string title)
        {
            Id = id;
            ProjectId = projectId;
            AuthorId = authorId;
            UserId = userId;
            Date = date;
            ExpiredDate = expiredDate;
            Message = message;
            Title = title;
        }
    }
}
