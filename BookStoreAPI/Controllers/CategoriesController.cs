using Microsoft.AspNetCore.Mvc;
using ReactAppTest.Server.Models;
using ReactStudentApp.Server.Services;

namespace ReactStudentApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger _logger;

        public CategoriesController(CategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var categories = await _categoryService.GetCategories();
                return Ok(new { message = "Fetch complete", data = categories });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching categories");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest(new { message = "Invalid category data" });
                }

                var success = await _categoryService.InsertCategory(category);
                if (success)
                    return Ok(new { message = "Insert complete" });

                return BadRequest(new { message = "Insert failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting category");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest(new { message = "Invalid category data" });
                }

                if (id != category.CategoryId)
                    return BadRequest(new { message = "Category ID mismatch" });

                var success = await _categoryService.UpdateCategory(id, category);
                if (success)
                    return Ok(new { message = "Update complete", data = category });

                return BadRequest(new { message = "Update failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _categoryService.DeleteCategory(id);
                if (success)
                    return Ok(new { message = "Delete complete" });

                return BadRequest(new { message = "Delete failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}
