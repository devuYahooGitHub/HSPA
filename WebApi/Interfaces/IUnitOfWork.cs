using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface IUnitOfWork
    {
         ICityRepository CityRepository{get;}
         Task<bool> SaveAsync();
    }
}