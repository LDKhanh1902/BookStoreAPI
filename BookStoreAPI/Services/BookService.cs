using Microsoft.EntityFrameworkCore;
using ReactAppTest.Server.Models;

namespace ReactAppTest.Server.Services
{
    public class BookService
    {
        private readonly DbModelContext dbModelContext;

        public BookService(DbModelContext dbModelContext)
        {
            this.dbModelContext = dbModelContext;
        }
        
        public async Task<List<Book>> GetBooks()
        {
            return await dbModelContext.Books.ToListAsync();
        }

        public async Task<bool> InsertBook(Book book)
        {
            try
            {
                if (book == null)
                {
                    return false;
                }

                var newBook = new Book()
                {
                    BookId = book.BookId,
                    AuthorId = book.AuthorId,
                    PublisherId = book.PublisherId,
                    CategoryId = book.CategoryId,
                    EntryDate = book.EntryDate,
                    PublishedDate = book.PublishedDate,
                    Quantity = book.Quantity,
                    Title = book.Title,
                    PurchasePrice = book.PurchasePrice,
                    Price = book.Price
                };

                dbModelContext.Books.Add(newBook);
                await dbModelContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateBook(int id,Book book)
        {
            try
            {
                if (book == null)
                {
                    return false;
                }

                var getBook = await dbModelContext.Books.FirstOrDefaultAsync(b=>b.BookId == id);

                if (getBook == null)
                    return false;

                getBook.BookId = book.BookId;
                getBook.AuthorId = book.AuthorId;
                getBook.PublisherId = book.PublisherId;
                getBook.CategoryId = book.CategoryId;
                getBook.EntryDate = book.EntryDate;
                getBook.PublishedDate = book.PublishedDate;
                getBook.Quantity = book.Quantity;
                getBook.Title = book.Title;
                getBook.PurchasePrice = book.PurchasePrice;
                getBook.Price = book.Price;

                await dbModelContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                var getBook = await dbModelContext.Books.FirstOrDefaultAsync(b => b.BookId == id);

                if (getBook == null)
                {
                    return false; // Không tìm thấy sách cần xóa
                }

                dbModelContext.Books.Remove(getBook);
                await dbModelContext.SaveChangesAsync();

                return true; // Xóa thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
