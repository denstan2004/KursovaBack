using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using KursovaBack.ResponceModels;
using KursovaBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace KursovaBack.Controllers
{
    [Route("api/Post")]
    public class PostController :Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [Route ("GetAll/authorized/{ProjectId}/{UserId}")]
        [HttpGet]
        public List<PostAuthorizedReturnModel> GetAllAsAuthorized ( Guid ProjectId, Guid UserId)
        {
            return _postRepository.GetAllAsAuthorized(ProjectId,UserId);
        }
        [Route("Like/post")]
        [HttpPost]
        public int LikePost([FromBody] LikePost likePost) 
        {
            _postRepository.LikePost(likePost.postId,likePost.userId);
            return _postRepository.GetPostLikes(likePost.postId);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] Post postViewModel)
        {
            if (postViewModel == null)
            {
                return BadRequest("Invalid post data");
            }

            byte[] imageData = null;
            if (postViewModel.fromFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await postViewModel.fromFile.CopyToAsync(ms);
                    imageData = ms.ToArray();
                }
            }

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = postViewModel.Title,
                Text = postViewModel.Text,
                ProjectId = postViewModel.ProjectId,
                AuthorId = postViewModel.AuthorId,
                Likes = 0,
                Image = imageData,
                Date = DateTime.Now
            };

            _postRepository.Create(post);

            return Ok();
        }
        [Route("GetAll/{projectId}")]
        [HttpGet]
        public List<Post> GetPosts(Guid projectId) 
        {
            return _postRepository.GetAllPostsByProject(projectId);
        }
        [Route("GetFull/{projectId}")]
        [HttpGet]
        public async Task<List<PostFullModel>> GetAllPostsFull(Guid projectId) 
        {
            var posts = await _postRepository.GetAllPostFullInfo(projectId);
            foreach (var post in posts)
            {
                try {
                    if (post.post.Image != null)
                    {
                       var imageBase64 = post.post.Image != null ? Convert.ToBase64String(post.post.Image) : null;
                        post.post.ImageBase64 = imageBase64;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return posts;
        }




    }
}
