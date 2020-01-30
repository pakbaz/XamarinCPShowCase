using CrossPlatformPOCShowcase.Core.Interfaces;
using CrossPlatformPOCShowcase.Core.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CrossPlatformPOCShowcase.API.Models
{
    public class SqlRepository : DbContext , IDataStore<Request>
    {
        /// <summary>
        /// Create our repositoty
        /// </summary>
        public SqlRepository(IConfiguration configuration) : base()
        {
            //Create database if not there. also does seeding
            Database.EnsureCreated();
        }
        public DbSet<Request> Requests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseOracle(@"Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = IP)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = pdb1)));" + "User Id= azureuser;Password=OraPasswd1;");
            //optionsBuilder.UseCosmos("url","key","CrossPlatform");
            optionsBuilder.UseSqlServer("ConnectionString");
        }


        private List<Request> _itemsCache;

        public async Task<bool> AddItemAsync(Request item)
        {
            try
            {
                Requests.Add(item);
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
                var item = await Requests.FirstOrDefaultAsync(x => x.id == id).ConfigureAwait(false);
                if (item == null)
                {
                    return false;
                }
                Requests.Remove(item);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Request> GetItemAsync(string id)
        {
            var item = await Requests.FirstOrDefaultAsync(x => x.id == id).ConfigureAwait(false);
            return item;
        }

        public async Task<IEnumerable<Request>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh || _itemsCache == null)
            {
                _itemsCache = await Requests.ToListAsync().ConfigureAwait(false);
            }
            return _itemsCache;
        }

        public async Task<bool> UpdateItemAsync(Request item)
        {
            try
            {
                Requests.Update(item);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
