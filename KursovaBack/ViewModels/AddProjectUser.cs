using KursovaBack.Models.Enums;

namespace KursovaBack.ViewModels
{
    public class AddProjectUser
    {
        public ProjectRole  role{ get; set; }
        public Guid userId { get; set;}
        public Guid projectId { get; set;}  

    }
}
