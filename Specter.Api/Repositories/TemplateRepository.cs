using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Specter.Api.Data.Entities;

namespace Specter.Api.Data.Repository
{
    public interface ITemplateRepository : IRepository<Template>
    {
        IQueryable<Template> GetForUser(Guid userId);
    }

    public class TemplateRepository : ITemplateRepository
    {
        private readonly IApplicationContext _context;

        public TemplateRepository(IApplicationContext context)
        {
            _context = context;
        }

        public Template GetById(params object[] ids)
        {
            return _context.Templates.Find(ids);
        }

        public IQueryable<Template> GetAll()
        {
            return _context.Templates.Where(ts => !ts.Deleted);
        }

        public void Insert(Template entity)
        {
            _context.Templates.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Template entity)
        {
            _context.Templates.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Template entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

        public IQueryable<Template> GetForUser(Guid userId)
        {
            return GetAll().Where(t => t.CreatedBy == userId || 
                                       t.Visibility == Visibility.Public || 
                                       t.CreatedByUser.Teams.SelectMany(ut => ut.Users)
                                                      .Any(u => u.UserId == userId));
        }
    }
}