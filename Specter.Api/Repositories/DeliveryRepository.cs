using System;
using System.Linq;

using Specter.Api.Data.Entities;

namespace Specter.Api.Data.Repository
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        IQueryable<Delivery> GetByProject(Guid projectId);
    }

    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly IApplicationContext _context;

        public DeliveryRepository(IApplicationContext context)
        {
            _context = context;
        }

        public Delivery GetById(params object[] ids)
        {
            return _context.Deliveries.Find(ids);
        }

        public IQueryable<Delivery> GetAll()
        {
            return _context.Deliveries.Where(d => !d.Removed).OrderBy(d => d.Order);
        }

        public void Insert(Delivery entity)
        {
            _context.Deliveries.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Delivery entity)
        {
            _context.Deliveries.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Delivery entity)
        {
            entity.Removed = true;
            Update(entity);
        }

        public IQueryable<Delivery> GetByProject(Guid projectId)
        {
            return GetAll().Where(d => d.ProjectId == projectId);
        }
    }
}