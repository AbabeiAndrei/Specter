using System.Linq;

using Specter.Api.Data.Entities;

namespace Specter.Api.Data.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationContext _context;

        public CategoryRepository(IApplicationContext context)
        {
            _context = context;
        }

        public Category GetById(params object[] ids)
        {
            return _context.Categories.Find(ids);
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories.Where(ts => !ts.Removed);
        }

        public void Insert(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Category entity)
        {
            entity.Removed = true;
            Update(entity);
        }
    }
}