using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using APPValper.Models;
using APPValper.Views;
using APPValper.Services;
using System.Collections.Generic;

namespace APPValper.ViewModels
{
    class OptionsViewModel : BaseViewModel
    {
        public Command HomeCommand { get; set; }
        public Command EnglishCommand { get; set; }
        public Command SpanishCommand { get; set; }
        INavigation Navigation;
        
        public OptionsViewModel(INavigation Nav)
        {
            Navigation = Nav;
            HomeCommand = new Command(async () => await Home(), () => !IsBusy);
            EnglishCommand = new Command(async () => await English(), () => !IsBusy);
            SpanishCommand = new Command(async () => await Spanish(), () => !IsBusy);
        }

        private async Task Home()
        {
            await Navigation.PushAsync(new ItemsPage());
        }

        private async Task English()
        {
            await Navigation.PushAsync(new ItemsPage());
        }

        private async Task Spanish()
        {
            await Navigation.PushAsync(new ItemsPage());
        }
    }
}
