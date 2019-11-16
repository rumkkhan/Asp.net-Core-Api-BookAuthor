using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Dtos;
using BookApi.Models;
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
        [HttpGet("{categoryId}",Name = "GetCategory")]
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

        //api/category/
        [HttpPost]
        [ProducesResponseType(422)]
        public IActionResult CreateCategory([FromBody]Category categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);
            var category = _categoryRepository.GetCategories()
                                                       .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                                                       .FirstOrDefault();
            if (category != null)
            {
                ModelState.AddModelError("", $"Country {category.Name} already exists");
                return StatusCode(422, $"Country {category.Name} already exists");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.CreateCategory(categoryCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {categoryCreate.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCategory", new { countryId = categoryCreate.Id }, categoryCreate);
        }

        //api/categories/countryId
        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, [FromBody] Category updatedCategoryInfo)
        {
            if (updatedCategoryInfo == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategoryInfo.Id)
                return BadRequest(ModelState);

            if (!_categoryRepository.IsDuplicateCategoryName(categoryId, updatedCategoryInfo.Name))
            {
                ModelState.AddModelError("", $"category {updatedCategoryInfo.Name} already exists");
            }
            if (!ModelState.IsValid)
                return NotFound(ModelState);
            if (!_categoryRepository.UpdateCategory(updatedCategoryInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedCategoryInfo.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //api/categories/countryId
        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCountry(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var categoryToDelete = _categoryRepository.GetCategory(categoryId);

            if (_categoryRepository.GetAllBooksForCategory(categoryId).Count() > 0)
            {
                ModelState.AddModelError("", $"category {categoryToDelete.Name}" +
                    "cannot be deleted because it is used at least one author");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {categoryToDelete.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}