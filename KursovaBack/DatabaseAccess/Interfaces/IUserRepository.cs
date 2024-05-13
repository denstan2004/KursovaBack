using KursovaBack.Models;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public void Update(User user);
        

      //  public void UpdateByVM(Guid id, UserUpdateVM user);

        public User GetByName(string name);
        public List<User> GetByNamePrefix(string name);

    }
}
