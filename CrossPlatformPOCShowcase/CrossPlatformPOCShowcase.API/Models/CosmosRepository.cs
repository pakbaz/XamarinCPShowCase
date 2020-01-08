using CrossPlatformPOCShowcase.Core.Interfaces;
using CrossPlatformPOCShowcase.Core.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CrossPlatformPOCShowcase.API.Models
{
    public class InMemoryRepository : DbContext , IDataStore<Item>
    {
        /// <summary>
        /// Create our repositoty
        /// </summary>
        public InMemoryRepository() : base()
        {
            //Create database if not there. also does seeding
            Database.EnsureCreated();
        }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseOracle(@"Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = IP)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = pdb1)));" + "User Id= azureuser;Password=OraPasswd1;");
            //optionsBuilder.UseCosmos("url","key","CrossPlatform");
            optionsBuilder.UseInMemoryDatabase("CrossPlatform");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //make ID property the key
            modelBuilder.Entity<Item>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Item>()
                .Property(p => p.Id)
                .HasValueGenerator<StringGuidGenerator>();
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


        private List<Item> _itemsCache;

        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                Items.Add(item);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                var item = await Items.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
                if (item == null)
                {
                    return false;
                }
                Items.Remove(item);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Item> GetItemAsync(string id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh || _itemsCache == null)
            {
                _itemsCache = await Items.ToListAsync().ConfigureAwait(false);
            }
            return _itemsCache;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                Items.Update(item);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }


    public class StringGuidGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next([NotNull] EntityEntry entry)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
