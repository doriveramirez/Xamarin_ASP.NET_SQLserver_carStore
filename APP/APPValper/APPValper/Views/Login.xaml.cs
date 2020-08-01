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
    public partial class Login : ContentPage
    {
        private LoginViewModel Context;
        public Login()
        {
            InitializeComponent();
            Context = new LoginViewModel(Navigation);
            BindingContext = Context;
        }
    }
}