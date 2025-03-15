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
                return Ok(new { message = "Fetch Complete", data = publishers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching publishers");
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
                    return BadRequest(new { message = "Invalid publisher data." });

                var result = await _publisherService.InsertPublisher(publisher);
                if (result)
                    return Ok(new { message = "Insert complete", data = publisher });
                
                return BadRequest(new { message = "Failed to insert publisher." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting publisher");
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
                    return BadRequest(new { message = "Publisher data is invalid." });

                var result = await _publisherService.UpdatePublisher(id, publisher);
                if (result)
                    return Ok(new { message = "Update complete", data = publisher });

                return BadRequest(new { message = "Failed to update publisher." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating publisher");
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
                    return Ok(new { message = "Delete successful" });

                return BadRequest(new { message = "Failed to delete publisher." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting publisher");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}
