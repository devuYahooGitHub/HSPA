using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ICityRepository
    {
         Task<IEnumerable<City>> GetCitiesAsync();

         void AddCity(City city);

         void DeleteCity(int CityId);

        // Task<bool> SaveAsync();

        Task<City> FindCity(int Id);
    }
}