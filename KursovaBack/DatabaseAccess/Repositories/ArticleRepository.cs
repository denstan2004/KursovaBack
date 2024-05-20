using KursovaBack.AppDbContext;
using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KursovaBack.DatabaseAccess.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly MyAppDbContext _context;

        public ArticleRepository(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Article entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Articles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public bool Delete(Guid id)
        {
            var itemToDelete = _context.Articles.FirstOrDefault(x => x.Id == id);

            if (itemToDelete != null)
            {
                _context.Articles.Remove(itemToDelete);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public Article Get(Guid id)
        {
            return _context.Articles.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Article>> GetAll()
        {
            return await _context.Articles.ToListAsync();
        }

        public void Update(Article entity)
        {
            _context.Articles.Update(entity);
            _context.SaveChanges();
        }
    }
}
