using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowingRecord> BorrowingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", IsBorrowed = false },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", IsBorrowed = true },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", IsBorrowed = false }
            );

            modelBuilder.Entity<User>().HasData(
              new User { Id = 1, Name = "User 1" },
              new User { Id = 2, Name = "User 2" },
              new User { Id = 3, Name = "User 3" }
          );

            modelBuilder.Entity<BorrowingRecord>().HasData(
                new BorrowingRecord { Id = 1, UserId = 1, BookId = 2, BorrowDate = DateTime.Now.AddDays(-10), ReturnDate = null },
                new BorrowingRecord { Id = 2, UserId = 2, BookId = 3, BorrowDate = DateTime.Now.AddDays(-5), ReturnDate = DateTime.Now.AddDays(-1) }
            );
        }
    }
}
