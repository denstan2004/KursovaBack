using KursovaBack.Models;
using KursovaBack.ResponceModels;


namespace project_back.DatabaseAccess.Interfaces
{
    public interface IVideoChatHubRepository : KursovaBack.DatabaseAccess.Interfaces.IBaseRepository<VideoChatHub>
    {
        public Task Update(VideoChatHub chatHub);

        public Task<VideoChatHub> GetAsync(Guid id);

        public Task<List<VideoChatHub>> GetHubsByUserId(Guid userId);

        public Task<VideoChatStatistics> GetPeriodStats(Guid psychologistId, int period);

        public Task<VideoChatHub> GetActiveHubByUsers(Guid user1Id, Guid user2Id);

        public Task CloseActiveCalls(Guid userId);
    }
}
