using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Microsoft.AspNetCore.Mvc;

namespace KursovaBack.Controllers
{
    [Route("api/Comment")]
    public class CommentController : Controller
    {
        ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        [HttpPost]
        [Route("Create")]
        public void Create([FromBody]Comment comment)
        {
            _commentRepository.Create(comment);
        }
        [HttpDelete]
        [Route("Delete/{commentId}/{userId}")]
        public void Delete(Guid commentId, Guid userId)
        {

        }
        [HttpGet]
        [Route("GetAll /{postId}")]
        public Task<List<Comment>> GetAllByPost(Guid postId)
        {
            return _commentRepository.GetAllByPost(postId);
        }
    }
}
