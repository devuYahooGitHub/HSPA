using System.Threading.Tasks;
using WebApi.Interfaces;

namespace WebApi.Data.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        //public ICityRepository CityRepository => throw new System.NotImplementedException();
        public ICityRepository CityRepository => new CityRepository(dc);

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<bool> SaveAsync()
        {
           // throw new System.NotImplementedException();
           return await dc.SaveChangesAsync() > 0;
        }
    }
}