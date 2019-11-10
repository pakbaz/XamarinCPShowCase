using CrossPlatformPOCShowcase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;


namespace CrossPlatformPOCShowcase.Models
{
    public class PocRepository : DbContext
    {
        private readonly string _dbPath;
        /// <summary>
        /// Create our repositoty
        /// </summary>
        /// <param name="dbPath">sqlite db path</param>
        public PocRepository(string dbPath) : base()
        {
            _dbPath = dbPath;

            //Create database if not there. also does seeding
            Database.EnsureCreated();
        }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //make ID property the key
            modelBuilder.Entity<Item>()
                .HasKey(p => p.Id);
            //require text to be set
            modelBuilder.Entity<Item>()
                .Property(p => p.Text)
                .IsRequired();
            modelBuilder.Entity<Item>()
                .Property(p => p.Category)
                .HasConversion(new EnumToStringConverter<ItemCategory>());


            //Add some initial data (Seeding)
#if DEBUG
            modelBuilder.Entity<Item>()
                .HasData(
                    new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", Category = ItemCategory.Work },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description = "This is an item description.", Category = ItemCategory.Shopping },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description = "This is an item description.", Category = ItemCategory.Personal },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description = "This is an item description.", Category = ItemCategory.Personal },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description = "This is an item description.", Category = ItemCategory.Shopping },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description = "This is an item description.", Category = ItemCategory.Work }
                );
#endif
        }
    }
}
