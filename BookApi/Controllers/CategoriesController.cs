using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Dtos;
using BookApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        //api/Categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categoriesDto = new List<CategoryDto>();
            foreach(var catagory in categories)
            {
                categoriesDto.Add(new CategoryDto()
                {
                    Id = catagory.Id,
                    Name = catagory.Name
                });
               
            }

            return Ok(categoriesDto);
        }

        //api/Categories/1
        [HttpGet("{categoryId}")]
        public IActionResult getCategory(int categoryId)
        {
            var category = _categoryRepository.GetCategory(categoryId);

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDto = new CategoryDto() { Id = category.Id, Name = category.Name};
            return Ok(categoryDto);
        }

        //api/categories/book/id
        [HttpGet("book/{bookId}")]
        public IActionResult GetCategoriesForABook(int bookId)
        {
            var categories = _categoryRepository.GetAllCategoriesForABook(bookId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto()
                {
                    Id= category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoriesDto);
        }


        //api/categories/categoryId/books
        [HttpGet("{categoryId}/books")]
        public IActionResult GetAllBooksForCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();
            var books = _categoryRepository.GetAllBooksForCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booksDto = new List<BookDto>();
            foreach (var book in books)
            {
                booksDto.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Isbn = book.Isbn,
                    DatePublished = book.DatePublished
                });
            }
            return Ok(booksDto);
        }
           
        }
}