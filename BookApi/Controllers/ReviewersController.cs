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
    public class ReviewersController : ControllerBase
    {

        private readonly IReviewerRepository _reviewersRepository;
        private readonly IReviewRepository  _reviewRepository;

        public ReviewersController(IReviewerRepository reviewerRepository, IReviewRepository reviewRepository)
        {
            _reviewersRepository = reviewerRepository;
            _reviewRepository = reviewRepository;
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
        
        [HttpGet("review/{reviewerId}",Name = "GetReviewer")]
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

        //api/reviewer/
        [HttpPost]
        [ProducesResponseType(422)]
        public IActionResult CreateReview([FromBody]Reviewer reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);          

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewersRepository.CreateReviewer(reviewerCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {reviewerCreate.FirstName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetReviewer", new { reviewerId = reviewerCreate.Id }, reviewerCreate);
        }
        //api/reviewer/reviewId
        [HttpPut("{reviewerId}")]
        [ProducesResponseType(422)]
        public IActionResult UpdateReview(int reviewerId, [FromBody]Reviewer reviewerToUpdate)
        {
            if (reviewerToUpdate == null)
              return BadRequest(ModelState);
            if (reviewerId != reviewerToUpdate.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_reviewersRepository.ReviewerExists(reviewerId))
                return NotFound();

            if (!_reviewersRepository.UpdateReviewer(reviewerToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {reviewerToUpdate.FirstName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //api/reviewer/reviewId
        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(422)]
        public IActionResult DeleteReview(int reviewerid)
        {
            if (!_reviewersRepository.ReviewerExists(reviewerid))
                return NotFound();
            var reviewerToDelete = _reviewersRepository.GetReviewer(reviewerid);
            var reviewesToDelete = _reviewersRepository.GetReviewsByReviewer(reviewerid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_reviewersRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong saving {reviewerToDelete.FirstName}");
                return StatusCode(500, ModelState);
            }
            if (!_reviewRepository.DeleteReviews(reviewesToDelete.ToList()))
            {
                ModelState.AddModelError("", $"Something went wrong saving reviews to delte");
                return StatusCode(500, ModelState); 
            }

            return Ok();
        }
    }
}