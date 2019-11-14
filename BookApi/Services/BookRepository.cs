﻿using BookApi.Models;
using BookApi.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _bookDbContext;

        public BookRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public bool BookExists(int bookId)
        {
            return  _bookDbContext.Books.Any(b => b.Id == bookId);
        }

        public bool BookExists(string bookIsbn)
        {
            return _bookDbContext.Books.Any(b => b.Isbn == bookIsbn);

        }

        public Book GetBook(int bookId)
        {
            return _bookDbContext.Books.Where(b => b.Id == bookId).SingleOrDefault();

        }

        public Book GetBook(string bookIsbn)
        {
            return _bookDbContext.Books.Where(b => b.Isbn == bookIsbn).SingleOrDefault();

        }

        public decimal GetBookRating(int bookId)
        {
            var reviews = _bookDbContext.Reviews.Where(r => r.Book.Id == bookId);
            if (reviews.Count() <= 0)
                return 0;

            return (decimal)reviews.Sum(r => r.Rating) / reviews.Count();
        }

        public ICollection<Book> GetBooks()
        {
            return  _bookDbContext.Books.OrderBy(b => b.Title).ToList();

        }

        public bool IsDuplicateIsbn(int bookId, string bookIsbn) 
        {
             var book =_bookDbContext.Books.Where(b => b.Isbn.Trim().ToUpper() == bookIsbn.Trim().ToUpper() && b.Id != bookId);
            return book == null ? false : true;
        }
    }
}
