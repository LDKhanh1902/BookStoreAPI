using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactAppTest.Server.Models;
using ReactAppTest.Server.Services;
using ReactStudentApp.Server.Services;
using System;
using System.Threading.Tasks;

namespace ReactStudentApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(AuthorService authorService, ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var authors = await _authorService.GetAuthors();
                return Ok(new { message = "Fetch complete", data = authors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching authors");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // POST api/Author
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Author author)
        {
            if (author == null)
                return BadRequest(new { message = "Invalid author data" });

            try
            {
                var success = await _authorService.InsertAuthor(author);
                if (success)
                    return Ok(new { message = "Insert complete", author });

                return BadRequest(new { message = "Insert was not successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAuthor");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // PUT api/Author/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Author author)
        {
            if (author == null)
                return BadRequest(new { message = "Invalid author data" });

            if (id != author.AuthorId)
                return BadRequest(new { message = "Author ID mismatch" });

            try
            {
                var success = await _authorService.UpdateAuthor(id, author);
                if (success)
                    return Ok(new { message = "Update complete", author });

                return BadRequest(new { message = "Update was not successful", author });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAuthor");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // DELETE api/Author/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _authorService.DeleteAuthor(id);
                if (success)
                    return Ok(new { message = "Delete complete", id });

                return BadRequest(new { message = "Delete was not successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAuthor");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}
