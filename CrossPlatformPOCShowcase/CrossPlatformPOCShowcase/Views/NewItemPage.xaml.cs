using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CrossPlatformPOCShowcase.Models;
using System.Linq;
using CrossPlatformPOCShowcase.Core.Models;
using CrossPlatformPOCShowcase.ViewModels;

namespace CrossPlatformPOCShowcase.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
       
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }

    }
}