using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using KursovaBack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KursovaBack.Controllers
{
    [Route("api/Task")]
    public class TaskController : Controller
    {
        ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpPost]
        [Route("Create")]
        public void Post([FromBody] PersonTask task)
        {
            _taskRepository.Create(task);
        }
        [HttpGet]
        [Route("Get/ByProject/{projectId}")]
        public Task<List<PersonTask>> GetTasks(Guid projectId)
        {
            return _taskRepository.GetAllTaskByProject(projectId);
        }
        [HttpGet]
        [Route("Get/ByUser/{projectId}/{userId}")]
        public Task<List<PersonTask>> GetTask(Guid projectId,Guid userId)
        {
            return _taskRepository.GetAllTaskByUser(projectId, userId);
        }
        [HttpPost]
        [Route("Update/Status")]
        public void UpdateStatus([FromBody] ChangeStatusVM statusVM)
        {
            _taskRepository.ChangeStatus(statusVM.TaskId, statusVM.Status);
        }


    }
}
