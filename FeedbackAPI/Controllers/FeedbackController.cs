using FeedbackAPI.Data;
using FeedbackAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbacksAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetFeedbacks()
        {
            try
            {
                var Feedbacks = _context.Feedbacks.ToList();
                if (Feedbacks.Count == 0)
                {
                    return NotFound("Feedbackss not available.");

                }
                return Ok(Feedbacks);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        public IActionResult SearchFeedbacks(int Id)
        {
            try
            {
                var Feedbacks = _context.Feedbacks.Find(Id);
                if (Feedbacks == null)
                {
                    return NotFound($"Feedbacks details not found with Id {Id}");

                }
                return Ok(Feedbacks);
            }


            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddFeedbacks(Feedback model)
        {
            try
            {
                _context.Feedbacks.Add(model);
                _context.SaveChanges();
                return Ok("Feedbacks Created");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult UpdateFeedbacks(Feedback model)
        {
            try
            {
                if (model == null || model.Id == 0)
                {
                    if (model == null)
                    {
                        return BadRequest("data is invalid");
                    }
                    else if (model.Id == 0)
                    {
                        return BadRequest($"Id {model.Id} is invalid");
                    }
                }
                var Feedbacks = _context.Feedbacks.Find(model.Id);
                if (Feedbacks == null)
                {
                    return NotFound($"Feedbacks not found with Id {model.Id}");
                }
                Feedbacks.Name = model.Name;
                Feedbacks.Rating = model.Rating;
                Feedbacks.Description = model.Description;
                _context.SaveChanges();
                return Ok("Feedbacks Updated");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]

        public IActionResult DeleteFeedbacks(int Id)
        {
            try
            {
                var Feedbacks = _context.Feedbacks.Find(Id);
                if (Feedbacks == null)
                {
                    return NotFound($"Feedbacks not found with Id {Id}");

                }
                _context.Feedbacks.Remove(Feedbacks);
                _context.SaveChanges();
                return Ok("Feedbacks details deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
