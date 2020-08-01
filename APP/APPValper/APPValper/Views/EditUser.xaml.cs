using System;
using APPValper.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPValper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUser : ContentPage
    {
        EditUserViewModel Context;
        public EditUser()
        {
            InitializeComponent();
            Context = new EditUserViewModel(Navigation);
            BindingContext = Context;
        }
    }
}