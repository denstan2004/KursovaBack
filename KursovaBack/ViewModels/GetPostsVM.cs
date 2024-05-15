namespace KursovaBack.ViewModels
{
    public class GetPostsVM
    {
       public Guid UserId { get; set; }
       public Guid ProjectId {  get; set; }

        public GetPostsVM(Guid userId, Guid projectId)
        {
            this.UserId = userId;
            this.ProjectId = projectId;
        }

        public GetPostsVM()
        {
        }
    }
}
