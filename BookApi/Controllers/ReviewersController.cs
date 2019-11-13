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
        [HttpGet("{reviewerId}")]
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
        
    }
}