using KursovaBack.Models;

namespace KursovaBack.ResponceModels
{
    public class PostFullModel
    {
        public  Task<List<Comment>> comments {  get; set; }
        public User User { get; set; }
        public Post post { get; set; }

        public PostFullModel(Task<List<Comment>> comments, User user, Post post)
        {
            this.comments = comments;
            User = user;
            this.post = post;
        }

        public PostFullModel()
        {
        }
    }
}
