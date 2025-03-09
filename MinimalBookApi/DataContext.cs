global using Microsoft.EntityFrameworkCore;

namespace MinimalBookApi
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NeuralFinanceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;");
        }

        public DbSet<Book> Books { get; set; }
    }
}