using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int countryId);
        Country GetCountryOfAuthor(int authorId);
        ICollection<Author> GetAuthorsFromCountry(int countryId);
        bool CountryExists(int contryId);
        bool IsDuplicateCountryName(int countryId, string countryName);


        //post 
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();
    }
}
