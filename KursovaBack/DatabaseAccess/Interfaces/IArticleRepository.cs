using KursovaBack.Models;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        public void Update(Article article);
    }
}
