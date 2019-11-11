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
            var countryDto = new List<CountryDto>();
            foreach (var country in countries)
            {
                countryDto.Add(new CountryDto
                {
                    Id = country.Id,
                    Name = country.Name
                }) ;
            }

            return Ok(countryDto);
        }
    }
}
