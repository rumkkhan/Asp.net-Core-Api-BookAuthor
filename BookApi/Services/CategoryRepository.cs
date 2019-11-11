using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;

namespace BookApi.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        public bool CategoryExists(int categoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Book> GetAllBooksForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public ICollection<Category> GetCategoriesForABook(int bookId) 
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
