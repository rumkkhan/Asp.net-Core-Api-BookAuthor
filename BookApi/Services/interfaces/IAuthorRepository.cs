using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services.interfaces
{
    public interface IAuthorRepository
    {
        ICollection<Author> GetAuthors();
        Author GetAuthor(int authorId);
        ICollection<Author> GetAuthorsOfBook(int bookId);
        ICollection<Book> GetBooksByAuthor(int authorId);
        bool AuthorExists(int authorId);
    }
}
