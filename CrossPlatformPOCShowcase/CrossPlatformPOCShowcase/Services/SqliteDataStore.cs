using CrossPlatformPOCShowcase.Core.Interfaces;
using CrossPlatformPOCShowcase.Core.Models;
using CrossPlatformPOCShowcase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformPOCShowcase.Services
{
    class SqliteDataStore : IDataStore<Item>
    {
        private List<Item> _itemsCache;

        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                App.Repository.Items.Add(item);
                await App.Repository.SaveChangesAsync().ConfigureAwait(false);
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
                var item = await App.Repository.Items.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
                if (item == null)
                {
                    return false;
                }
                App.Repository.Items.Remove(item);
                await App.Repository.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Item> GetItemAsync(string id)
        {
            var item = await App.Repository.Items.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh || _itemsCache == null)
            {
                _itemsCache = await App.Repository.Items.ToListAsync().ConfigureAwait(false);
            }
            return _itemsCache;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                App.Repository.Items.Update(item);
                await App.Repository.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
