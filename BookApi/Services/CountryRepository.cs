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

        public bool CreateCountry(Country country)
        {
            _context.AddAsync(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
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

        public bool IsDuplicateCountryName(int countryId, string countryName)
        {
            var country = _context.Conutry.Where(c => c.Name.Trim().ToUpper() == countryName.Trim().ToUpper() && c.Id != countryId);
            return country == null ? false : true;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
