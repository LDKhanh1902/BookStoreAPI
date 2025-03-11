using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactAppTest.Server.Models;
using ReactAppTest.Server.Services;
using System;
using System.Threading.Tasks;

namespace ReactAppTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var books = await _bookService.GetBooks();
                return Ok(new { message = "Fetch complete", data = books });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // POST api/Book
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Book book)
        {
            if (book == null)
                return BadRequest(new { message = "Invalid book data" });

            try
            {
                var success = await _bookService.InsertBook(book);
                if (success)
                    return Ok(new { message = "Insert complete", data = book });

                return BadRequest(new { message = "Insert failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting book");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // PUT api/Book/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Book book)
        {
            if (book == null)
                return BadRequest(new { message = "Invalid book data" });

            if (id != book.BookId)
                return BadRequest(new { message = "Book ID mismatch" });

            try
            {
                var success = await _bookService.UpdateBook(id, book);
                if (success)
                    return Ok(new { message = "Update complete", data = book });

                return BadRequest(new { message = "Update failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // DELETE api/Book/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _bookService.DeleteBook(id);
                if (success)
                    return Ok(new { message = "Delete complete", id });

                return BadRequest(new { message = "Delete failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}
