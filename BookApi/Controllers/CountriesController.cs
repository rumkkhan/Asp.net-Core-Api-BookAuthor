using BookApi.Dtos;
using BookApi.Models;
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

        [HttpGet("{countryId}", Name = "GetCountry")]
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

        //api/controller/auther/authorid
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
    
        //api/countries/countryId/authors
        [HttpGet("{countryId}/authors")]
        public IActionResult GetAuthorsFromCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();
            var authors = _countryRepository.GetAuthorsFromCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var authorDto = new List<AuthorDto>();
            foreach (var author in authors)
            {
                authorDto.Add(new AuthorDto{
                     Id = author.Id,
                     FirstName = author.FirstName,
                     LastName = author.LastName
                });
            }
            return Ok(authorDto);
        }

        //api/countries/
        [HttpPost]
        [ProducesResponseType(422)]
        public IActionResult CreateCountry([FromBody]Country countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);
            var country = _countryRepository.GetCountries()
                                                       .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
                                                       .FirstOrDefault();
            if (country != null)
            {
                ModelState.AddModelError("", $"Country {country.Name} already exists");
                return StatusCode(422, $"Country {country.Name} already exists");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           if(!_countryRepository.CreateCountry(countryCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {countryCreate.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCountry", new { countryId = countryCreate.Id }, countryCreate);   
        }

        //api/countries/countryId
        [HttpPut("{countryId}")]
        public IActionResult UpdateCountry(int countryId, [FromBody] Country updatedCountryInfo)
        {
            if (updatedCountryInfo == null)
                return BadRequest(ModelState);

            if (countryId != updatedCountryInfo.Id)
                return BadRequest(ModelState);
            
            if(!_countryRepository.IsDuplicateCountryName(countryId, updatedCountryInfo.Name))
            {
                ModelState.AddModelError("", $"Country {updatedCountryInfo.Name} already exists");
            }
            if (!ModelState.IsValid)
                return NotFound(ModelState);
            if (!_countryRepository.UpdateCountry(updatedCountryInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedCountryInfo.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{countryId}")]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var countryToDelete = _countryRepository.GetCountry(countryId);

            if(_countryRepository.GetAuthorsFromCountry(countryId).Count() > 0)
            {
                ModelState.AddModelError("", $"Country {countryToDelete.Name}" +
                    "cannot be deleted because it is used at least one author");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {countryToDelete.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
