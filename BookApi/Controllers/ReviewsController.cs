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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        //api/reviews
        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews =  _reviewRepository.GetReviews();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDto = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewDto.Add(new ReviewDto
                {
                     Id = review.Id,
                     HeadLine = review.HeadLine,
                     Rating  = review.Rating,
                     ReviewText = review.ReviewText
                });
            }
            return Ok(reviewDto);
        }

        //api/reviews/1
        [HttpGet("{reviewId}")]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();
            var review = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDto = new ReviewDto() { HeadLine = review.HeadLine, Id = review.Id, Rating = review.Rating, ReviewText = review.ReviewText };
            return Ok(reviewDto);

        }


        //api/reivews/book/bookid
        [HttpGet("book/{bookId}")]
        public IActionResult GetReviewsForABook(int bookId)
        {
            var reviews = _reviewRepository.GetReviewsOfABook(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDto = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewDto.Add(new ReviewDto
                {
                     Id = review.Id,
                      HeadLine  = review.HeadLine,
                      Rating = review.Rating,
                      ReviewText = review.ReviewText
                });
            }
            return Ok(reviewDto);
        }

        //api/reviews/book/reviewId
        [HttpGet("review/{reviewId}")]
        public IActionResult GetBookOfAReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var book = _reviewRepository.GetBookOfAReview(reviewId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDto = new BookDto { Id = book.Id, DatePublished = book.DatePublished, Title = book.Title };

            return Ok(bookDto);
        }
    }
}