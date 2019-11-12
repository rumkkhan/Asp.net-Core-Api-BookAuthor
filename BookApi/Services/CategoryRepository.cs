using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;

namespace BookApi.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookDbContext _categoryContext;
        public CategoryRepository(BookDbContext categoryContext)
        {
            _categoryContext = categoryContext;
        }
        public bool CategoryExists(int categoryId)
        {
           return _categoryContext.Categorys.Any(c => c.Id == categoryId);
        }

        public ICollection<Book> GetAllBooksForCategory(int categoryId)
        {
            return _categoryContext.BookCategories.Where(c => c.CategoryId == categoryId).Select(b => b.Book).ToList();
        }

        public ICollection<Category> GetCategories()
        {
            return _categoryContext.Categorys.OrderBy(c => c.Name).ToList();
        }

        public ICollection<Category> GetAllCategoriesForABook(int bookId) 
        {
            return _categoryContext.BookCategories.Where(b => b .BookId == bookId).Select(c => c.Category).ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _categoryContext.Categorys.Where(c => c.Id == categoryId).FirstOrDefault();
        }
    }
}
