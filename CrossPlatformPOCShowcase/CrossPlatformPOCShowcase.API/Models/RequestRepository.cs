using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossPlatformPOCShowcase.Core.Interfaces;
using CrossPlatformPOCShowcase.Core.Models;


namespace CrossPlatformPOCShowcase.API.Models
{
    public class RequestRepository : IDataStore<Request>
    {
        readonly List<Request> _items;

        public RequestRepository()
        {
            _items = new List<Request>()
            {
                new Request { id = Guid.NewGuid().ToString(), name = "First item", description="This is an item description.", deleted= false }
            };
               
        }

        public async Task<bool> AddItemAsync(Request item)
        {
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Request item)
        {
            var oldItem = _items.Where((Request arg) => arg.id == item.id).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.Where((Request arg) => arg.id == id).FirstOrDefault();
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Request> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<Request>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }
    }
}