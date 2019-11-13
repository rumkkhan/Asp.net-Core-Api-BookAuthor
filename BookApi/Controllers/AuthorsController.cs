using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Dtos;
using BookApi.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        //api/authors
        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = _authorRepository.GetAuthors();

            if (!ModelState.IsValid)
                return BadRequest();

            var authorDto = new List<AuthorDto>();

            foreach (var author in authors)
            {
                authorDto.Add(new AuthorDto
                {
                         Id = author.Id,
                         FirstName = author.FirstName,
                         LastName = author.LastName
                });
            }

            return Ok(authorDto);
        }

        //api/authors/authorId
        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
                return NotFound();

            var author = _authorRepository.GetAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest();

            var authorDto = new AuthorDto() { Id = author.Id, FirstName = author.FirstName, LastName = author.LastName };

            return Ok(authorDto);
        }

        //api/authors/books/authorId
        [HttpGet("book/{authorId}")]
        public IActionResult GetBookByAuthor(int authorId)
        {
            if (!_authorRepository.AuthorExists(authorId))
                      return NotFound();

            var books = _authorRepository.GetBooksByAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booksDto = new List<BookDto>();
            foreach (var book in books)
            {
                booksDto.Add(new BookDto
                {
                    Id = book.Id,
                    DatePublished = book.DatePublished,
                    Title = book.Title
                }); 
            }

            return Ok(booksDto);
        }
        
        //api/authors/authors/bookId
        [HttpGet("authors/{bookId}")]
        public IActionResult GetAuthorsOfBook(int bookId)
        {
         

            var authors = _authorRepository.GetAuthorsOfBook(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorDto = new List<AuthorDto>();
            foreach (var author in authors)
            {
                authorDto.Add(new AuthorDto
                {
                     Id = author.Id,
                     FirstName = author.FirstName,
                     LastName = author.LastName
                });
            }

            return Ok(authorDto);
        }
    }
}