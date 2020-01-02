using System;
using CrossPlatformPOCShowcase.Core.Models;
using CrossPlatformPOCShowcase.Models;
using Xamarin.Forms;

namespace CrossPlatformPOCShowcase.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(INavigation nav , Item item = null)
        {
            if (item == null)
            {
                nav.PopModalAsync();
            }


            Title = item?.Text;
            Item = item;

            DeleteCommand = new Command(async () =>
            {
                MessagingCenter.Send(this, "DeleteItem", Item);
                await nav.PopToRootAsync();
            });
        }


        public Command DeleteCommand { get; set; }
    }
}
