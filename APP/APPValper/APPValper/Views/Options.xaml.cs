using APPValper.ViewModels;
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
    public partial class Options : ContentPage
    {
        OptionsViewModel Context;
        public Options()
        {
            InitializeComponent();
            Context = new OptionsViewModel(Navigation);
            BindingContext = Context;
        }
    }
}