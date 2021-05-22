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
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        //private readonly DataContext dc;
        //private readonly ICityRepository CityRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        //public CityController(DataContext dc, ICityRepository CityRepository)
        // public CityController(ICityRepository CityRepository)
        //public CityController(IUnitOfWork uow)
        public CityController(IUnitOfWork uow,IMapper mapper)
        {
            //this.CityRepository = CityRepository;
            
            //this.uow = uow;
            
            this.uow = uow;
            this.mapper = mapper;

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
            throw new UnauthorizedAccessException();
            //var cities = await dc.Cities.ToListAsync();
            
            // var cities = await uow.CityRepository.GetCitiesAsync();
            // return Ok(cities);

            var cities = await uow.CityRepository.GetCitiesAsync();

            // var cityDto = from c in cities
            // select new CityDto{
            //     Id=c.Id,
            //     Name=c.Name
            // };

            var cityDto = mapper.Map<IEnumerable<CityDto>>(cities);
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

            // var city = new City{
            //     Name = cityDto.Name,
            //     LastUpdatedBy =1,
            //     LastUpdatedOn= DateTime.Now
            // };
            // if(!ModelState.IsValid){
            //     return BadRequest(ModelState);
            // }
            var city=mapper.Map<City>(cityDto);
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id,CityDto cityDto){
            
            throw new Exception("Some unknown error occured in code");
            if (id !=cityDto.Id) 
                return BadRequest("Update not allowed");

            var cityFromDb = await uow.CityRepository.FindCity(id);

            if(cityFromDb==null)
                return BadRequest("Update not allowed");
            
            cityFromDb.LastUpdatedBy=1;
            cityFromDb.LastUpdatedOn= DateTime.Now;

            mapper.Map(cityDto,cityFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpPut("updateCityName/{id}")]
        public async Task<IActionResult> UpdateCity(int id,CityUpdateDto cityDto){
            var cityFromDb = await uow.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedBy=1;
            cityFromDb.LastUpdatedOn= DateTime.Now;

            mapper.Map(cityDto,cityFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCityPatch(int id,JsonPatchDocument<City> cityToPatch){
            var cityFromDb = await uow.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedBy=1;
            cityFromDb.LastUpdatedOn= DateTime.Now;

            cityToPatch.ApplyTo(cityFromDb,ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }
    }
}