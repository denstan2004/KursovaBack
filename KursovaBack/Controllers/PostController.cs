using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using KursovaBack.ResponceModels;
using KursovaBack.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public void LikePost(Guid postId, Guid userId) 
        {
            _postRepository.LikePost(postId,userId);
        }

        [Route("Create")]
        [HttpPost]
        public void CreatePost([FromBody] Post post)
        {
            _postRepository.Create(post);
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
            return posts;
        }




    }
}
