using Microsoft.EntityFrameworkCore;
using ReactAppTest.Server.Models;

namespace ReactStudentApp.Server.Services
{
    public class CategoryService
    {
        private readonly DbModelContext _dbModelContext;

        public CategoryService(DbModelContext dbModelContext)
        {
            _dbModelContext = dbModelContext;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _dbModelContext.Categories.ToListAsync();
        }

        public async Task<bool> InsertCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return false;
                }

                var newCategory = new Category()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };
                _dbModelContext.Categories.Add(newCategory);
                await _dbModelContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateCategory(int id, Category category)
        {
            try
            {
                var existingCategory = await _dbModelContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                if (existingCategory == null)
                {
                    return false;
                }

                existingCategory.CategoryName = category.CategoryName;
                existingCategory.Description = category.Description;
                await _dbModelContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                var category = await _dbModelContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category == null)
                {
                    return false;
                }

                _dbModelContext.Categories.Remove(category);
                await _dbModelContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
