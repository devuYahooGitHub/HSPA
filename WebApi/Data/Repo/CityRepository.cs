using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext dc;

        public CityRepository(DataContext dc)
        {
            this.dc = dc;
        }
        
        public void AddCity(City city)
        {
            //throw new System.NotImplementedException();
            dc.Cities.Add(city);
        }

        public void DeleteCity(int CityId)
        {
            //throw new System.NotImplementedException();
            var city = dc.Cities.Find(CityId);
                dc.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
           // throw new System.NotImplementedException();
           return await dc.Cities.ToListAsync();
        }

        // public async Task<bool> SaveAsync()
        // {
        //     //throw new System.NotImplementedException();
        //     return await dc.SaveChangesAsync() > 0;
        // }
    }
}