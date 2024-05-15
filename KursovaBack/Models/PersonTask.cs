namespace KursovaBack.Models
{
    public class PersonTask
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }

        public PersonTask()
        {
        }

        public PersonTask(Guid id, Guid projectId, Guid authorId, Guid userId, DateTime date, DateTime expiredDate, string message, string title, string status)
        {
            Id = id;
            ProjectId = projectId;
            AuthorId = authorId;
            UserId = userId;
            Date = date;
            ExpiredDate = expiredDate;
            Message = message;
            Title = title;
            Status = status;
        }
    }
}
