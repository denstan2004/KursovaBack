using KursovaBack.DatabaseAccess.Interfaces;
using project_back.Models;

namespace project_back.DatabaseAccess.Interfaces
{
    public interface IInvestmentRequestRepository : IBaseRepository<InvestmentRequest>
    {
        Task<List<InvestmentRequest>> GetAllByProject(Guid projectId);
        void ChangeStatus(Guid requestId, string status);
    }
}
