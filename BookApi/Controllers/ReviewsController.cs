using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Dtos;
using BookApi.Models;
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
        private readonly IBookRepository _bookRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewsController(IReviewRepository reviewRepository, IBookRepository bookRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _reviewerRepository = reviewerRepository;
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
        [HttpGet("{reviewId}",Name = "GetReview")]
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
        //api/review/
        [HttpPost]
        [ProducesResponseType(422)]
        public IActionResult CreateReview([FromBody]Review reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            if (!_reviewerRepository.ReviewerExists(reviewCreate.Reviewer.Id))
                ModelState.AddModelError("", "Reviewer doesn't exist");

            if (!_bookRepository.BookExists(reviewCreate.Book.Id))
                ModelState.AddModelError("", "Book doesn't exist");

            if (!ModelState.IsValid)
                return  StatusCode(404,ModelState);


            reviewCreate.Reviewer = _reviewerRepository.GetReviewer(reviewCreate.Reviewer.Id);
            reviewCreate.Book = _bookRepository.GetBook(reviewCreate.Reviewer.Id);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.CreateReview(reviewCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {reviewCreate.Reviewer}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetReview", new { reviewId = reviewCreate.Id }, reviewCreate); 
        }
        //api/review/
        [HttpPut("{reviewId}")]
        [ProducesResponseType(422)]
        public IActionResult  UpdateReview(int reviewId, [FromBody]Review reviewToUpdate)
        {
            if (reviewToUpdate == null)
                return BadRequest(ModelState);
            if (reviewId != reviewToUpdate.Id)
                return BadRequest(ModelState);

            if (!_reviewerRepository.ReviewerExists(reviewToUpdate.Reviewer.Id))
                ModelState.AddModelError("", "Review doesn't exist");

            if (!_bookRepository.BookExists(reviewToUpdate.Book.Id))
                ModelState.AddModelError("", "Book doesn't exist");

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);


            reviewToUpdate.Reviewer = _reviewerRepository.GetReviewer(reviewToUpdate.Reviewer.Id);
            reviewToUpdate.Book = _bookRepository.GetBook(reviewToUpdate.Reviewer.Id);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.UpdateReview(reviewToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {reviewToUpdate.Reviewer}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        //api/review/
        [HttpPut("{reviewId}")]
        [ProducesResponseType(422)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var reviewToDelete = _reviewRepository.GetReview(reviewId);
            if(reviewToDelete == null)

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong saving review");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}