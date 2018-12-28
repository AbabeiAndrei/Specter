using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Specter.Api.Data.Repository
{
    public interface IRepository<T> 
        where T : class
    {
        IQueryable<T> GetAll();
        T GetById(params object[] ids);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}