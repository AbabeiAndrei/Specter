using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Specter.Api.Data.Entities;

namespace Specter.Api.Data.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        
    }

    public class ProjectRepository : IProjectRepository
    {
        private readonly IApplicationContext _context;

        public ProjectRepository(IApplicationContext context)
        {
            _context = context;
        }

        public Project GetById(params object[] ids)
        {
            return _context.Projects.Find(ids);
        }

        public IQueryable<Project> GetAll()
        {
            return _context.Projects.Where(ts => !ts.Removed);
        }

        public void Insert(Project entity)
        {
            _context.Projects.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Project entity)
        {
            _context.Projects.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Project entity)
        {
            entity.Removed = true;
            Update(entity);
        }
    }
}