using Microsoft.AspNetCore.Mvc;
using ReactStudentApp.Server.Services;
using ReactAppTest.Server.Models;

namespace ReactStudentApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _publisherService;
        private readonly ILogger _logger;

        public PublishersController(PublisherService publisherService, ILogger<PublishersController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        // GET all publishers
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var publishers = await _publisherService.GetPublisher();
                return Ok(new {message = "Fecth Complate",data= publishers});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // POST api/<PublishersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Publisher publisher)
        {
            try
            {
                if (publisher == null)
                    return BadRequest("Invalid publisher data.");

                var result = await _publisherService.InsertPublisher(publisher);
                if (result)
                    return CreatedAtAction(nameof(Get), new { id = publisher.PublisherId }, publisher);
                return BadRequest("Failed to insert publisher.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // PUT api/<PublishersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Publisher publisher)
        {
            try
            {
                if (publisher == null || id != publisher.PublisherId)
                    return BadRequest("Publisher data is invalid.");

                var result = await _publisherService.UpdatePublisher(id, publisher);
                if (result)
                    return NoContent(); // Successfully updated
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // DELETE api/<PublishersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _publisherService.DeletePublisher(id);
                if (result)
                    return NoContent(); // Successfully deleted
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}
