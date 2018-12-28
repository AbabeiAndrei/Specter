using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Specter.Api.Data.Entities;

namespace Specter.Api.Data.Repository
{
    public interface ITimesheetRepository : IRepository<Timesheet>
    {
        
    }

    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly IApplicationContext _context;

        public TimesheetRepository(IApplicationContext context)
        {
            _context = context;
        }

        public Timesheet GetById(params object[] ids)
        {
            return _context.Timesheets.Find(ids);
        }

        public IQueryable<Timesheet> GetAll()
        {
            return _context.Timesheets.Where(ts => !ts.Removed);
        }

        public void Insert(Timesheet entity)
        {
            _context.Timesheets.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Timesheet entity)
        {
            _context.Timesheets.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Timesheet entity)
        {
            entity.Removed = true;
            Update(entity);
        }
    }
}