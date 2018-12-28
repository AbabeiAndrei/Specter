namespace Specter.Api.Data
{
    public interface IDbContext
    {
        void SaveChanges();
        void Rollback();
    }
}