using CrossPlatformPOCShowcase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CrossPlatformPOCShowcase.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public NewItemViewModel()
        {
            SaveCommand = new Command(async() =>
            {
                MessagingCenter.Send(this, "AddItem", new Item { Text = Text, Category = (ItemCategory)Category, Description = Description});
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });
            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });
        }

        public List<string> Categories => Enum.GetNames(typeof(ItemCategory)).ToList();
        
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        private int _category;
        public int Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }


        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
    }
}
