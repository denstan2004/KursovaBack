using KursovaBack.DatabaseAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using project_back.DatabaseAccess.Interfaces;
using project_back.Models;
using project_back.ViewModels;

namespace project_back.Controllers
{
    [Route("api/Requests")]
    public class InvestmentRequestController : Controller
    {
        IInvestmentRequestRepository _RequestRepository;

        public InvestmentRequestController(IInvestmentRequestRepository RequestRepository)
        {
            _RequestRepository = RequestRepository;
        }
        [Route("GetAll/{projectId}")]
        [HttpGet]
        public async Task<List<InvestmentRequest>> GetAllByProject(Guid projectId)
        {
            var req = await _RequestRepository.GetAllByProject(projectId);
            return req.ToList();
        }
        [Route("Create")]
        [HttpPost]
        public void Create([FromBody] InvestmentRequest request) 
        {
            _RequestRepository.Create(request);
        }
        [HttpPost]
        [Route("Change/status/")]
        public void ChangeStatus([FromBody] ChangeRequestStatus change)
        {
            _RequestRepository.ChangeStatus(change.requestId, change.status);
        }
        [HttpGet]
        [Route("GetAll/ByInvestor/{userId}")]
        public async Task<List<InvestmentRequest>> GetAllByInvestor(Guid userId)
        {
            List<InvestmentRequest> req = await _RequestRepository.GetAll();
            List<InvestmentRequest> requests = new List<InvestmentRequest>();
            foreach (var item in req)
            {
               if(item.UserID== userId) requests.Add(item);
            }
            return requests;
        }

    }
}
