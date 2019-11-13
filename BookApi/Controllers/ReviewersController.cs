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
    public class ReviewersController : ControllerBase
    {

        private readonly IReviewerRepository _reviewersRepository;

        public ReviewersController(IReviewerRepository reviewerRepository)
        {
            _reviewersRepository = reviewerRepository;
        }

        //api/reviewer
        [HttpGet]
        public IActionResult GetReviewers()
        {
            var reviewers = _reviewersRepository.GetReviewers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewersDto = new List<ReviewerDto>();
            foreach (var reviewer in reviewers)
            {
                reviewersDto.Add(new ReviewerDto
                {
                     Id = reviewer.Id,
                     FirstName = reviewer.FirstName,
                     LastName  = reviewer.LastName
                });
            }
            return Ok(reviewersDto);

        }

        //api/reviewer/1
        [HttpGet("review{reviewerId}")]
        public IActionResult GetReview(int reviewerId)
        {

            var reviewer = _reviewersRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return NotFound(ModelState);

            var reviewerDto = new ReviewerDto()
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };

            return Ok(reviewerDto);

        }

        //api/reviewer/review/1
        [HttpGet("reviews/{reviewerId}")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var reviews = _reviewersRepository.GetReviewsByReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDto = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewDto.Add(new ReviewDto { Id = review.Id, HeadLine = review.HeadLine, Rating = review.Rating, ReviewText = review.ReviewText });
            }

            return Ok(reviewDto);
        }

        //api/reviewer/1/reviewer
        [HttpGet("reviewer/{reviewId}")]
        public IActionResult GetReviewerOfAReview(int reviewId)
        {
            var reviewer = _reviewersRepository.GetReviewerOfAReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerDto = new ReviewerDto() { Id = reviewer.Id, FirstName = reviewer.FirstName, LastName = reviewer.LastName };

            return Ok(reviewerDto);
        }
    }
}