using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Npgsql;
using static Dapper.SqlMapper;
using System.Data;
using KursovaBack.ResponceModels;
using Microsoft.Extensions.Hosting;
using KursovaBack.Models.Enums;
using System.Collections.Generic;

namespace KursovaBack.DatabaseAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";
        ICommentRepository _commentRepository;
        IUserRepository _userRepository;

        public PostRepository(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public PostRepository()
        {
        }

        async System.Threading.Tasks.Task IBaseRepository<Post>.Create(Post entity)
        {
            Guid postId = Guid.NewGuid();
            int likes = 0;
            entity.Id = postId;
            entity.Likes = likes;
            DateTime date = DateTime.Now;
            entity.Date = date;
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
               
                
                try
                {
                    var sqlQuery = "INSERT INTO posts (id, title,text,project_id,author_id,likes,date,image ) VALUES(@Id, @Title,@Text,@ProjectId,@AuthorId,@Likes,@Date,@image)";
                    db.Execute(sqlQuery, entity);
                    
                }
                catch (Exception ex)
                {

                }
            }
        }

        public bool Delete(Guid id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    var sqlQuery = "Delete From posts where id=@id";
                    db.Execute(sqlQuery, id);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public  Post Get(Guid id)
        {
            
                using (IDbConnection db = new NpgsqlConnection(connectionString))
                {
                    return db.Query<Post>("Select id, title, text, project_id as ProjectId, author_id as AuthorId, likes, image, date FROM Posts Where id=@id ", new { id }).ToList().FirstOrDefault();
                }
            
        }
        public async Task<List<Post>> GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var posts = await db.QueryAsync<Post>("SELECT id, title, text, project_id as ProjectId, author_id as AuthorId, likes, image, date FROM Posts");
                return posts.ToList();
            }
        }


        public List<Post> GetAllPostsByProject(Guid projectId)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {


                    var posts =  db.Query<Post>("SELECT * FROM posts where project_id=@projectId", new {projectId});
                    return posts.ToList();
                }
                catch (Exception ex)
                {
                    return new List<Post>();
                }
            }
        }

        public List<PostAuthorizedReturnModel> GetAllAsAuthorized(Guid projectId, Guid userId)
        {
            List<Post> posts = new List<Post>();
            PostRepository repository = new PostRepository();
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {


                    var Posts = db.Query<Post>("SELECT * FROM posts where project_id=@projectId", new { projectId });
                    posts= Posts.ToList();
                }
                catch (Exception ex)
                {
                     posts=new List<Post>();
                }
            }
            PostAuthorizedReturnModel post1 = new PostAuthorizedReturnModel();
            List<PostAuthorizedReturnModel> postAuthorizedReturns = new List<PostAuthorizedReturnModel>();
            if(posts.Count > 0)
            {
                 foreach (var post in posts)
                {
            
                    post1.Id = post.Id;
                    post1.AuthorId = post.AuthorId;
                    post1.ProjectId = projectId;
                    post1.Date=post.Date;
                    post1.Title=post.Title;
                    post1.Text=post.Text;
                    post1.Likes=post.Likes;
                    post1.Image = post.Image;
                    post1.IsLiked=repository.IsLiked(userId, post.Id);
                    postAuthorizedReturns.Add(post1);
                }
                
            }
            return postAuthorizedReturns;
        }
        public bool IsLiked(Guid userId,Guid postId)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {


                   Guid userIdPresent =db.Query<Guid>("SELECT user_id FROM likes_users where post_id =@postId and user_id=@userId  ", new { postId,userId }).ToList().FirstOrDefault();
                    if(userIdPresent== Guid.Empty) return false;
                    else return  true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public void LikePost(Guid postId, Guid userId)
        {
            PostRepository postsRepository = new PostRepository();
            bool isLiked = postsRepository.IsLiked(userId, postId);
            if (!isLiked)
            {
                using (IDbConnection db = new NpgsqlConnection(connectionString))
                {
                  string sqlQuery = "insert into likes_users(post_id,user_id) values(@postId,@userId)";
                    db.Execute(sqlQuery, new { postId, userId });
                    string sqlQuery2 = "UPDATE posts set likes=likes+1 where id=@postId";
                    db.Execute(sqlQuery2, new { postId });
                }
            }
            else
            {
                using (IDbConnection db = new NpgsqlConnection(connectionString))
                {
                    string sqlQuery = "Delete from likes_users where post_id=@postId and user_id=@userId";
                    db.Execute(sqlQuery, new { postId, userId });
                    string sqlQuery2 = "UPDATE posts set likes=likes-1 where id=@postId";
                    db.Execute(sqlQuery2, new { postId});
                }
            }
        }

        public async Task< PostFullModel> GetPostFullInfo(Guid postId)
        {

            PostRepository postRepository = new PostRepository();
            Post post = postRepository.Get(postId);
            Task<List<Comment>> comments = _commentRepository.GetAllByPost(postId);
            await comments;
            User user = _userRepository.Get(post.AuthorId);
            try
            {
                if (user.Avatar != null)
                {
                    var imageBase64 = user.Avatar != null ? Convert.ToBase64String(user.Avatar) : null;
                    user.ImageBase64 = imageBase64;
                }
            }
            catch (Exception ex) { }
            PostFullModel model = new PostFullModel(comments, user, post);
            return model;
        }

        public async Task<List<PostFullModel>>  GetAllPostFullInfo(Guid projectId)
        {
            PostRepository postRepository = new PostRepository();
            List<Post> posts=  postRepository.GetAllPostsByProject(projectId);
            List<PostFullModel> postFullModels = new List<PostFullModel>();
            foreach (Post post in posts)
            {
                PostFullModel postFullModel = await GetPostFullInfo(post.Id);
                postFullModels.Add(postFullModel);
            }
            return postFullModels;
        }

        public int GetPostLikes(Guid postId)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {


                     int likes = db.Query<int>("SELECT likes FROM posts where id=@postId", new { postId }).ToList().FirstOrDefault();
                    return likes;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
    }
}
