using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Data.Repo;
using WebApi.Models;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        //private readonly DataContext dc;
        //private readonly ICityRepository CityRepository;
        private readonly IUnitOfWork uow;

        //public CityController(DataContext dc, ICityRepository CityRepository)
        // public CityController(ICityRepository CityRepository)
        public CityController(IUnitOfWork uow)
        {
            //this.CityRepository = CityRepository;
            this.uow = uow;

            // this.dc = dc;
        }

        //[HttpGet("GetTModels")]
        //http://localhost:5000/api/city/GetTModels
        // public IEnumerable<string> GetTModels()
        // {
        //     return new string[] { "Atlanta", "New York", "Chicago" };
        // }

        //http://localhost:5000/api/city/
        //default is HttpGet
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            //var cities = await dc.Cities.ToListAsync();
            
            // var cities = await uow.CityRepository.GetCitiesAsync();
            // return Ok(cities);

            var cities = await uow.CityRepository.GetCitiesAsync();

            var cityDto = from c in cities
            select new CityDto{
                Id=c.Id,
                Name=c.Name
            };
            return Ok(cityDto);
        }


        //post api/city/add?cityname=Washington
        [HttpPost("add")]
        //post api/city/add/Los Angeles
        [HttpPost("add/{cityname}")]
        public async Task<IActionResult> AddCities(string cityName)
        {
            City city = new City();
            city.Name = cityName;
            // await dc.Cities.AddAsync(city);
            // await dc.SaveChangesAsync();
            //return Ok(city);
            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        //post api/city/post
        //http://localhost:5000/api/city/post
        // post man header Content-Type
        // application/json; charset=UTF-8
        [HttpPost("post")]
        //public async Task<IActionResult> AddCities(City city)
        public async Task<IActionResult> AddCities(CityDto cityDto)
        {
            // await dc.Cities.AddAsync(city);
            // await dc.SaveChangesAsync();
            //return Ok(city);

            // uow.CityRepository.AddCity(city);
            // await uow.SaveAsync();
            // return StatusCode(201);

            var city = new City{
                Name = cityDto.Name,
                LastUpdatedBy =1,
                LastUpdatedOn= DateTime.Now
            };
             uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        //post api/city/post
        //http://localhost:5000/api/city/delete/7
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCities(int id)
        {
            // var city = await dc.Cities.FindAsync(id);
            // dc.Cities.Remove(city);
            // await dc.SaveChangesAsync();
            // return Ok(id);
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return StatusCode(201);
        }
    }
}