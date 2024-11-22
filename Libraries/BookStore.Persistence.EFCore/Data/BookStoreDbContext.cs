using BookStore.Persistence.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence.EFCore.Data;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<InventoryBalance> InventoryBalances { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Relationship Setup

        // !: Book -> Author relationship.
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.SetNull);

        // !: FIXES -> SQLite Error 19: 'NOT NULL constraint failed: Books.AuthorId'.
        modelBuilder.Entity<Book>()
            .Property(b => b.AuthorId)
            .IsRequired(false);

        // !: Author -> Book relationship.
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.SetNull);

        // !: InventoryBalance -> Composite key.
        modelBuilder.Entity<InventoryBalance>()
            .HasKey(ib => new { ib.StoreId, ib.Isbn13 });

        // !: InventoryBalance -> Store relationship.
        modelBuilder.Entity<InventoryBalance>()
            .HasOne(ib => ib.Store)
            .WithMany(s => s.InventoryBalances)
            .HasForeignKey(ib => ib.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        // !: InventoryBalance -> Book relationship.
        modelBuilder.Entity<InventoryBalance>()
            .HasOne(ib => ib.Book)
            .WithMany(b => b.InventoryBalances)
            .HasForeignKey(ib => ib.Isbn13)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Seed Data

        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Austen",
                BirthDate = DateTime.Parse("1775-12-16")
            },
            new Author
            {
                Id = 2,
                FirstName = "George",
                LastName = "Orwell",
                BirthDate = DateTime.Parse("1903-06-25")
            },
            new Author
            {
                Id = 3,
                FirstName = "Mark",
                LastName = "Twain",
                BirthDate = DateTime.Parse("1835-11-30")
            },
            new Author
            {
                Id = 4,
                FirstName = "Virginia",
                LastName = "Woolf",
                BirthDate = DateTime.Parse("1882-01-25")
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Isbn13 = "9780141439518",
                Title = "Pride and Prejudice",
                Language = "English",
                Price = 9.99m,
                Pages = 279,
                ReleaseDate = DateTime.Parse("1813-01-28"),
                AuthorId = 1
            },
            new Book
            {
                Isbn13 = "9780141439471",
                Title = "Sense and Sensibility",
                Language = "English",
                Price = 9.99m,
                Pages = 392,
                ReleaseDate = DateTime.Parse("1811-01-01"),
                AuthorId = 1
            },
            new Book
            {
                Isbn13 = "9780451524935",
                Title = "1984",
                Language = "English",
                Price = 14.99m,
                Pages = 328,
                ReleaseDate = DateTime.Parse("1949-06-08"),
                AuthorId = 2
            },
            new Book
            {
                Isbn13 = "9781451628963",
                Title = "The Adventures of Tom Sawyer",
                Language = "English",
                Price = 7.99m,
                Pages = 274,
                ReleaseDate = DateTime.Parse("1876-01-01"),
                AuthorId = 3
            },
            new Book
            {
                Isbn13 = "9780099511408",
                Title = "Mrs. Dalloway",
                Language = "English",
                Price = 12.99m,
                Pages = 194,
                ReleaseDate = DateTime.Parse("1925-05-14"),
                AuthorId = 4
            }
        );

        modelBuilder.Entity<Store>().HasData(
            new Store
            {
                Id = 1,
                Name = "Adlibris",
                Address = "Kungsgatan 34, 411 19 Göteborg"
            },
            new Store
            {
                Id = 2,
                Name = "Akademibokhandeln",
                Address = "Nordstan, Norra Hamngatan 26, 411 06 Göteborg"
            },
            new Store
            {
                Id = 3,
                Name = "Campusbokhandeln",
                Address = "Götabergsgatan 17 411 34 Göteborg"
            }
        );

        modelBuilder.Entity<InventoryBalance>().HasData(
            new InventoryBalance
            {
                StoreId = 1,
                Isbn13 = "9780141439518", // Pride and Prejudice
                Quantity = 10
            },
            new InventoryBalance
            {
                StoreId = 1,
                Isbn13 = "9780141439471", // Sense and Sensibility
                Quantity = 3
            },
            new InventoryBalance
            {
                StoreId = 2,
                Isbn13 = "9780141439518", // Pride and Prejudice
                Quantity = 5
            },
            new InventoryBalance
            {
                StoreId = 2,
                Isbn13 = "9780451524935", // 1984
                Quantity = 15
            },
            new InventoryBalance
            {
                StoreId = 2,
                Isbn13 = "9781451628963", // The Adventures of Tom Sawyer
                Quantity = 7
            },
            new InventoryBalance
            {
                StoreId = 3,
                Isbn13 = "9780099511408", // Mrs. Dalloway
                Quantity = 8
            }
        );

        #endregion
    }
}