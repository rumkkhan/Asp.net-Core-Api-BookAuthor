using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;

namespace BookApi.Services
{
    public class CountryRepository : ICountryRepository
    {

        private readonly BookDbContext _context;
        public CountryRepository(BookDbContext context)
        {
            _context = context;
        }

        public bool CountryExists(int countryId)
        {
            return _context.Conutry.Any(c => c.Id == countryId);
        }

        public ICollection<Author> GetAuthorsFromCountry(int countryId)
        {
            return  _context.Authors.Where(c => c.Country.Id == countryId).ToList();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Conutry.OrderBy(c => c.Name).ToList();
        }

        public Country GetCountry(int countryId)
        {
            return _context.Conutry.Where(c => c.Id == countryId).FirstOrDefault();
        }

        public Country GetCountryOfAuthor(int authorId)
        {
            return _context.Authors.Where(a => a.Id == authorId).Select(c => c.Country).FirstOrDefault();
        }
    }
}
