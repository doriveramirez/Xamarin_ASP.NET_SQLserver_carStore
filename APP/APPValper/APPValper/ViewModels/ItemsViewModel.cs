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
    public class ItemsViewModel : BaseViewModel
    {
        OptionsService service = new OptionsService();

        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command CRUDCommand { get; set; }
        public Command ChangeUserCommand { get; set; }
        public Command SaveCommand { get; set; }
        public Command HelpCommand { get; set; }
        public Command ExitCommand { get; set; }

        public Command OptionsCommand { get; set; }
        public Command SpanishCommand { get; set; }
        public Command EnglishCommand { get; set; }
        INavigation Navigation;

        public Connection Con { get; set; }
        public string Url { get; set; }
        public Language Language { get; set; }
        private string LanguageSelected;

        public string ButtonCRUDText { get; set; }
        public string ButtonLoginText { get; set; }
        public string LabelTitle { get; set; }
        public string LabelDescription { get; set; }
        public string ButtonExitText { get; set; }
        public string ButtonHelpText { get; set; }
        public string TitleSettings { get; set; }
        public string TitleHome { get; set; }
        public string UrlText { get; set; }
        public string chooseLanguageText { get; set; }



        public ItemsViewModel(INavigation Nav)
        {
            Update();
            Navigation = Nav;
            EnglishCommand = new Command(async () => await English(), () => !IsBusy);
            SpanishCommand = new Command(async () => await Spanish(), () => !IsBusy);
            SaveCommand = new Command(async () => await Save(), () => !IsBusy);
            OptionsCommand = new Command(async () => await Options(), () => !IsBusy);
            CRUDCommand = new Command(async () => await CRUD(), () => !IsBusy);
            ChangeUserCommand = new Command(async () => await ChangeUser(), () => !IsBusy);
            HelpCommand = new Command(Help);
            ExitCommand = new Command(Exit);
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
            CheckLanguage();
        }

        private void CheckLanguage()
        {
            if (!string.IsNullOrEmpty(LanguageSelected))
            {
                if (LanguageSelected.Equals("Spanish"))
                {
                    Title = "Pantalla principal";
                    ButtonCRUDText = "CRUD";
                    if (service.ConsultUser().Logged)
                    {
                        ButtonLoginText = "Desconectarse";
                    }
                    else
                    {
                        ButtonLoginText = "Conectarse";
                    }
                    LabelTitle = "Valper Soluciones Y Mantenimientos, S.L.";
                    LabelDescription = "Asesoramiento, suministro, mantenimiento y soporte de sistemas informáticos. Desarrollo de Software a medida. Comercialización de Software para el sector de la automoción. Ya existe acuerdo de distribución y mantenimiento con empresa multinacional. Desarrollo de aplicaciones de integración para el software anteriormente mencionado para ser distribuidos a nivel nacional.";
                    ButtonHelpText = "Ayuda";
                    ButtonExitText = "Salir";
                    TitleSettings = "Opciones";
                    TitleHome = "Página principal";
                    UrlText = "Direccion IP";
                    chooseLanguageText = "Elije un idioma";
    }
                else
                {
                    Title = "Home";
                    ButtonCRUDText = "CRUD";
                    if (service.ConsultUser().Logged)
                    {
                        ButtonLoginText = "Log out";
                    }
                    else
                    {
                        ButtonLoginText = "Login";
                    }
                    LabelTitle = "Valper Solutions and Maintenance, S.L.";
                    LabelDescription = "Advice, supply, maintenance and support of computer systems. Custom software development. Software Marketing for the automotive sector. There is already a distribution and maintenance agreement with a multinational company. Development of integration applications for the aforementioned software to be distributed nationwide.";
                    ButtonHelpText = "Help";
                    ButtonExitText = "Exit";
                    TitleSettings = "Settings";
                    TitleHome = "Home";
                    UrlText = "IP Address";
                    chooseLanguageText = "Choose language";
                }
            }
        }

        private void Update()
        {
            Url = service.ConsultLocal().Url;
            LanguageSelected = service.ConsultLanguage().Name;
        }

        private async Task Save()
        {
            bool successful;
            IsBusy = true;
            Con = new Connection()
            {
                Url = Url
            };
            if (string.IsNullOrEmpty(Con.Url))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La dirección no puede ser nula", "Aceptar");
            }
            else
            {
                Console.WriteLine(Con.Url);
                successful = await service.SaveLocalAsync(Con);
                if (successful)
                {
                    await Application.Current.MainPage.DisplayAlert("Felicidades", "Se ha conectado satisfactoriamente a la url: " + Con.Url, "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido conectar a la url: " + Con.Url, "Aceptar");
                }
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task English()
        {
            IsBusy = true;
            Language = new Language()
            {
                Name = "English"
            };
            Console.WriteLine("holaasd");
            service.SaveLanguage(Language);
            (Application.Current).MainPage = new MainPage();
            await Application.Current.MainPage.DisplayAlert("Atención", "Se ha cambiado el idioma a inglés.", "Aceptar");
            await Task.Delay(2000);
            IsBusy = false;
            //EnglishFrame.BackgroundColor = Color.Yellow;
            //SpanishFrame.BackgroundColor = Color.White;
        }

        private async Task Spanish()
        {
            IsBusy = true;
            Language = new Language()
            {
                Name = "Spanish"
            };
            service.SaveLanguage(Language);
            (Application.Current).MainPage = new MainPage();
            await Application.Current.MainPage.DisplayAlert("Atención", "Se ha cambiado el idioma a español.", "Aceptar");
            await Task.Delay(2000);
            IsBusy = false;
            //EnglishFrame.BackgroundColor = Color.White;
            //SpanishFrame.BackgroundColor = Color.Yellow;
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CRUD()
        {
            if (service.ConsultUser().Logged)
            {
                await Navigation.PushAsync(new Functions());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe estar conectado para acceder", "Aceptar");
            }

        }

        private async Task Options()
        {
            await Navigation.PushAsync(new Options());
        }

        private async Task ChangeUser()
        {
            if (service.ConsultUser().Logged)
            {
                var user = service.ConsultUser();
                user.Logged = false;
                service.SaveUser(user);
                await Application.Current.MainPage.DisplayAlert("Felicidades", "Se ha desconectado correctamente", "Aceptar");
                (Application.Current).MainPage = new MainPage();
            }
            else
            {
                await Navigation.PushAsync(new Login());
            }
            
        }

        private void Help()
        {
            Device.OpenUri(new Uri(Url + "/ManualDeAyuda/Index.html"));
        }

        private void Exit()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

    }
}