using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;
using KursovaBack.Models.Enums;

namespace KursovaBack.ViewModels
{
    public class UserRegisterVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }


        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Roles Role { get; set; } = 0;
    }
 
}
