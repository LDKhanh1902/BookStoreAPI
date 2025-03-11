using Microsoft.EntityFrameworkCore;
using ReactAppTest.Server.Models;

namespace ReactStudentApp.Server.Services
{
    public class PublisherService
    {
        private readonly DbModelContext _dbModelContext;

        public PublisherService(DbModelContext dbModelContext)
        {
            _dbModelContext = dbModelContext;
        }

        public async Task<List<Publisher>> GetPublisher()
        {
            return await _dbModelContext.Publishers.ToListAsync();
        }

        public async Task<bool> InsertPublisher(Publisher publisher)
        {
            try
            {
                if (publisher == null)
                    return false;

                var newPublisher = new Publisher()
                {
                    PublisherId = publisher.PublisherId,
                    PublisherName = publisher.PublisherName,
                    Contact = publisher.Contact
                };

                _dbModelContext.Publishers.Add(newPublisher);
                await _dbModelContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        // Update Method
        public async Task<bool> UpdatePublisher(int id,Publisher publisher)
        {
            try
            {
                if (publisher == null)
                    return false;

                var existingPublisher = await _dbModelContext.Publishers
                    .FirstOrDefaultAsync(p => p.PublisherId == id);

                if (existingPublisher == null)
                    return false;

                // Update fields
                existingPublisher.PublisherName = publisher.PublisherName;
                existingPublisher.Contact = publisher.Contact;

                await _dbModelContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        // Delete Method
        public async Task<bool> DeletePublisher(int publisherId)
        {
            try
            {
                var publisher = await _dbModelContext.Publishers
                    .FirstOrDefaultAsync(p => p.PublisherId == publisherId);

                if (publisher == null)
                    return false;

                _dbModelContext.Publishers.Remove(publisher);
                await _dbModelContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
