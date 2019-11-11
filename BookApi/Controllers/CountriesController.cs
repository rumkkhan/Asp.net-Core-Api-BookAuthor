using BookApi.Dtos;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController :  ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries().ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var countriesDto = new List<CountryDto>();
            foreach (var country in countries)
            {
                countriesDto.Add(new CountryDto
                {
                    Id = country.Id,
                    Name = country.Name
                }) ;
            }

            return Ok(countriesDto);
        }

        [HttpGet("{countryId}")]
        public IActionResult GetCountry(int countryId)
        {

            if (!_countryRepository.CountryExists(countryId))
                return NotFound(); 
            var country = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var countryDto = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };
          

            return Ok(countryDto);
        }

        //api/controlelr/auther/authorid
        [HttpGet("author/{autherId}")]
        public IActionResult GetCountryOfAuthor(int autherId)
        {

            //validate the author exists
            var country = _countryRepository.GetCountryOfAuthor(autherId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var countryDto = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };


            return Ok(countryDto);
        }
    }
}
