using Microsoft.EntityFrameworkCore;
using ReactAppTest.Server.Models;

namespace ReactStudentApp.Server.Services
{
    public class AuthorService
    {
        private readonly DbModelContext _dbModelContext;

        public AuthorService(DbModelContext dbModelContext)
        {
            _dbModelContext = dbModelContext;
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _dbModelContext.Authors.ToListAsync();
        }

        public async Task<bool> InsertAuthor(Author author)
        {
            try
            {
                var newAuthor = new Author()
                {
                    AuthorId = author.AuthorId,
                    Name = author.Name,
                    BirthDate = author.BirthDate,
                    Nationality = author.Nationality
                };

                _dbModelContext.Authors.Add(newAuthor);
                await _dbModelContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateAuthor(int id,Author author)
        {
            try
            {
                var updatedAuthor = await _dbModelContext.Authors.FirstOrDefaultAsync(a=>a.AuthorId == id);
                if (updatedAuthor != null)
                {
                    updatedAuthor.AuthorId = author.AuthorId;
                    updatedAuthor.Name = author.Name;
                    updatedAuthor.BirthDate = author.BirthDate;
                    updatedAuthor.Nationality = author.Nationality;

                    await _dbModelContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            try
            {
                var author = await _dbModelContext.Authors.FirstOrDefaultAsync(a=>a.AuthorId==id);
                if (author != null)
                {
                    _dbModelContext.Remove(author);
                    await _dbModelContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
