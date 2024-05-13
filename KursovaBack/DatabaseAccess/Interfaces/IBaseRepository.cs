namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task Create(T entity);
        public T Get(Guid id);
        public bool Delete(Guid id);
        public Task<List<T>> GetAll();
    }
}
