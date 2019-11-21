using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Dtos;
using BookApi.Models;
using BookApi.Services;
using BookApi.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookRepository _bookDbContext;
        private readonly IEmployee _employee;

        public BooksController(IBookRepository bookDbContext, IEmployee employee)
        {
            _bookDbContext = bookDbContext;
            _employee = employee;
        }

        //api/books 
        [HttpGet]
        public IActionResult GetBooks()
        {
           var books =  _bookDbContext.GetBooks();

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

        //api/books/bookId
        [HttpGet("{bookId}")]
        public IActionResult GetBook(int bookId)
        {
            if (!_bookDbContext.BookExists(bookId))
                return NotFound();
            var book = _bookDbContext.GetBook(bookId);

            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                DatePublished = book.DatePublished,
                Isbn = book.Isbn
            };

            return Ok(bookDto);

        }

        //api/books/bookIs
        [HttpGet("rating/{bookId}")]
        public IActionResult GetBookRating(int bookId)
        {

            if (!_bookDbContext.BookExists(bookId))
                return NotFound();

            var rating = _bookDbContext.GetBookRating(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }
        //api/books/bookIs
        [HttpPost]
        public async Task<IActionResult> AddData()
        {
            var personCount = 99999;
            var personList = new List<Employee>();
            

            for (int i = 1; i < personCount; i++)
            {
                personList.Add(new Employee
                {
                     Id = i,
                     Company = "IT"+ i,
                     Name   = "Ramzan"+ i+ 1,
                     Designation = "Software engineer",
                     a = "aaaaaaaaaaaaaaaaaaaaaa",
                     b ="asdfasdfa",
                     c = "asdfasdf",
                     d = "aee",
                     e = "adfadfv",
                     f = "aa",
                     g = "adfasdfasdf",
                     h = "asdfaf",
                     i = "eedd",
                     j = "asdfe"
                     
,
                });
             }
            bool result = await _employee.save(personList);
            return Ok(result);
        }
    }
}