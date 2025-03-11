using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace ReactAppTest.Server.Models
{
    public class DbModelContext(DbContextOptions<DbModelContext> options) : DbContext(options)
    {
        public required DbSet<Author> Authors { get; set; }
        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Publisher> Publishers { get; set; }
        public required DbSet<Book> Books { get; set; }
        public required DbSet<Employee> Employees { get; set; }
        public required DbSet<Position> Positions { get; set; }
        public required DbSet<PaymentDetail> PaymentDetails { get; set; }
        public required DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=turntable.proxy.rlwy.net;port=31630;database=railway;user=root;password=TXgXIsArOZyxTRnAzSUnSFlQHTspvKFd;",
                new MySqlServerVersion(new Version(8, 0, 23)));
        }
    }
}
