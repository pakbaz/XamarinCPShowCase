using System;
using CrossPlatformPOCShowcase.Core.Models;
using CrossPlatformPOCShowcase.Models;

namespace CrossPlatformPOCShowcase.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
