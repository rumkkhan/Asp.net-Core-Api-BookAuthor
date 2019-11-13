using BookApi.Models;
using BookApi.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookDbContext _reivewDbContext;
        public ReviewRepository(BookDbContext bookDbContext)
        {
            _reivewDbContext = bookDbContext;
        }
        public Book GetBookOfAReview(int reviewId)
        {
            var bookId = _reivewDbContext.Reviews.Where(r => r.Id == reviewId).Select(b => b.Book.Id).FirstOrDefault();
            return _reivewDbContext.Books.Where(b => b.Id == bookId).FirstOrDefault();
        }

        public Review GetReview(int reviewId)
        {
            return _reivewDbContext.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public  ICollection<Review> GetReviews()
        {
            return _reivewDbContext.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfABook(int bookId)
        {
            return _reivewDbContext.Reviews.Where(r => r.Book.Id == bookId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _reivewDbContext.Reviews.Any(r => r.Id == reviewId);
        }
    }
}
