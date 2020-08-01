using APPValper.ViewModels;
using APPValper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPValper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Functions : TabbedPage
    {
        FunctionsViewModel Context = new FunctionsViewModel();
        public Functions()
        {
            InitializeComponent();
            BindingContext = Context;
            LvCars.ItemSelected += LvCars_ItemSelected;
            LvBrands.ItemSelected += LvBrands_ItemSelected;
        }

        private void LvCars_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Car model = (Car)e.SelectedItem;
                Context.Model = model.Model;
                Context.Power = model.Power;
                Context.Color = model.Color;
                Context.Ndoor = model.Ndoor;
                Context.IdCar = model.Id;
                Context.BrandID = model.BrandID;
            }
        }

        private void LvBrands_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Brand model = (Brand)e.SelectedItem;
                Context.Name = model.Name;
                Context.Headquarters = model.Headquarters;
                Context.Founder = model.Founder;
                Context.BrandID = model.Id;
            }
        }
    }
}