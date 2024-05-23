using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.DatabaseAccess.Repositories;
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
        public async Task<List<Project>> GetAllProjects() 
        {
           var projects  = await _projectRepository.GetAll();
            
              return convert(projects);
           
        }
        [Route("id/{id}")]
        [HttpGet]
        public Project GetProject(Guid id)
        {
            var project = _projectRepository.Get(id);
            try
            {
                if (project.Image != null)
                {
                    var imageBase64 = project.Image != null ? Convert.ToBase64String(project.Image) : null;
                    project.ImageBase64 = imageBase64;
                }
            }
            catch (Exception ex)
            {

            }
            return project;
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
        public async Task<List<Project>> GetAllByCategory(string category)
            {
            var projects = await _projectRepository.GetAllByCategory(category);

            return convert( projects);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromForm] ProjectCreateViewModel model) 
        {
            byte[] imageData = null;
            if (model.fromFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await model.fromFile.CopyToAsync(ms);
                    imageData = ms.ToArray();
                }
            }
            model.Image = imageData;
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
        [Route("Get/User/projects/{userId}")]
        public List<Project> GetUserProjects (Guid userId)
        {
            var projects= _projectRepository.GetUserProjects(userId);

            return convert (projects);
        }
        [HttpGet]
        [Route("GetAll/Members/{projectId}")]
        public async Task<IActionResult> GetAllUsersByProject(Guid projectId)
        {
            var projects = await _projectRepository.GetAllByProject(projectId);
            foreach (var user in projects)
            {
                try
                {
                    if (user.Avatar != null)
                    {
                        var imageBase64 = user.Avatar != null ? Convert.ToBase64String(user.Avatar) : null;
                        user.ImageBase64 = imageBase64;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return Ok(projects);

        }

        public static List<Project> convert(List<Project> projects)
        {
            foreach (var project in projects)
            {
                try
                {
                    if (project.Image != null)
                    {
                        var imageBase64 = project.Image != null ? Convert.ToBase64String(project.Image) : null;
                        project.ImageBase64 = imageBase64;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return projects;
        }

    }
}
