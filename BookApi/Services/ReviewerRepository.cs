using BookApi.Models;
using BookApi.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly BookDbContext _reviewerDbContext;
        public ReviewerRepository(BookDbContext bookDbContext)
        {
            _reviewerDbContext = bookDbContext;
        }
        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _reviewerDbContext.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();

        }

        public Reviewer GetReviewer(int reviewerId)
        {
           return   _reviewerDbContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }

        public Reviewer GetReviewerOfAReview(int reviewId)
        {
            var reviewerId = _reviewerDbContext.Reviews.Where(r => r.Id == reviewId).Select(rr => rr.Reviewer.Id).FirstOrDefault();
            return _reviewerDbContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
           return  _reviewerDbContext.Reviewers.OrderBy(r => r.LastName).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _reviewerDbContext.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _reviewerDbContext.Add(reviewer);
            return Save();
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _reviewerDbContext.Update(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _reviewerDbContext.Remove(reviewer);
            return Save();
        }

        public bool Save()
        {
            var saved = _reviewerDbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

       
    }
}
