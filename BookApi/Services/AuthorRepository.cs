using BookApi.Models;
using BookApi.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookDbContext _bookDbContext;
        public AuthorRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        public bool AuthorExists(int authorId)
        {
            return _bookDbContext.Authors.Any(a => a.Id == authorId);
        }

        public Author GetAuthor(int authorId)
        {
            return _bookDbContext.Authors.Where(a => a.Id == authorId).SingleOrDefault();
        }

        public ICollection<Author> GetAuthors()
        {
            return _bookDbContext.Authors.ToList();
        }

        public ICollection<Author> GetAuthorsOfBook(int bookId)
        {
            return _bookDbContext.BookAuthors.Where(b => b.Book.Id == bookId).Select(a => a.Author).ToList();
        }

        public ICollection<Book> GetBooksByAuthor(int authorId)
        {
            return _bookDbContext.BookAuthors.Where(a => a.Author.Id == authorId).Select(b => b.Book).ToList();
        }
    }
}
