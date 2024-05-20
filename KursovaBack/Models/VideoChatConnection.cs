using System.ComponentModel.DataAnnotations;

namespace KursovaBack.Models
{
    public class VideoChatConnection
    {
        [Key]
        public string ConnectionId { get; set; }

        public Guid UserId { get; set; }

    }
}
