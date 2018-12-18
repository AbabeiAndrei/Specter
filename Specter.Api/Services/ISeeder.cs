using System.Threading.Tasks;

namespace Specter.Api.Services
{
    public interface ISeeder
    {
        Task Seed();
    }
}