using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using KursovaBack.Services.Implementations;
using KursovaBack.Services.Interfaces;
using KursovaBack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KursovaBack.Controllers
{
    [Route("api/Project")]
    public class ProjectController :Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectService _projectService;

        public ProjectController(IUserRepository userRepository, IProjectRepository projectRepository,IProjectService projectService)
        {
            _projectService=projectService;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }
        [Route ("GetAll")]
        [HttpGet]
        public Task<List<Project>> GetAllProjects() 
        {
           var projects = _projectRepository.GetAll();
            
              return projects;
           
        }
        [Route("GetAll/Categories")]
        [HttpGet]
        public List<string> GetCategories ()
        {
            var categories = _projectRepository.GetAllCategories();
            return categories;
        }
        [Route("GetAll/{category}")]
        [HttpGet]
        public Task<List<Project>> GetAllByCategory(string category)
        {
            var projects = _projectRepository.GetAllByCategory(category);
            return projects;
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectCreateViewModel model) 
        {
          
          _projectService.CreateProject(model, model.CreatorId);
           
            return Ok();
        }
        [Route("Add/Student")]
        [HttpPost]
        public IActionResult AddProjectStudent([FromBody] AddProjectUser model)
        {
            model.role = Models.Enums.ProjectRole.Basic;


            _projectRepository.AddUserToProject(model);
           

            return Ok();
        }
        [Route("Add/Mentor")]
        [HttpPost]
        public IActionResult AddProjectMentor([FromBody] AddProjectUser model)
        {
            model.role = Models.Enums.ProjectRole.Mentor;
            _projectRepository.AddUserToProject(model);
            return Ok();
        }

        [Route("Delete/Member/{id}")]
        [HttpDelete]
        public IActionResult DeleteUserFromProject(Guid id)
        {
            _projectRepository.DeleteUserById(id);
            return Ok();
        }
        [HttpGet]
        [Route("GetAll/Members/{projectId}")]
        public IActionResult GetAllUsersByProject(Guid projectId)
        {
            var projects =_projectRepository.GetAllByProject(projectId);
            return Ok(projects);
        }



    }
}
