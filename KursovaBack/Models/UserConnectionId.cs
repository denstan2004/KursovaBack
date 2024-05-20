using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace KursovaBack.Models
{
    public class UserConnectionId
    {
        [Key]
        public string ConnectionId { get; set; }

        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }

        public Guid HubId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public bool IsConnected { get; set; } = false;
    }
}
