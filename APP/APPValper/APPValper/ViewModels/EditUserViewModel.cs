using APPValper.Models;
using APPValper.Services;
using APPValper.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace APPValper.ViewModels
{
    class EditUserViewModel : User
    {

        OptionsService service = new OptionsService();
        public Language Language { get; set; }
        public string Url { get; private set; }

        private string LanguageSelected;
        public string EditUserTittle { get; set; }
        public string ChangeImageButtonText { get; set; }
        public string ActualNameText { get; set; }
        public string ChangeNameButtonText { get; set; }
        public string ActualPasswordText { get; set; }
        public string NewPasswordText { get; set; }
        public string ConfirmPasswordText { get; set; }
        public string ChangePasswordButtonText { get; set; }
        public Command ChangeNameCommand { get; set; }
        public Command ChangePasswordCommand { get; set; }
        INavigation Navigation;

        public string Name { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }

        public EditUserViewModel(INavigation Nav)
        {
            Navigation = Nav;
            Update();
            CheckLanguage();
            ChangeNameCommand = new Command(async () => await ChangeName());
            ChangePasswordCommand = new Command(async () => await ChangePassword());
        }

        private void CheckLanguage()
        {
            if (!string.IsNullOrEmpty(LanguageSelected))
            {
                if (LanguageSelected.Equals("Spanish"))
                {
                    EditUserTittle = "Configuración de usuario";
                    ChangeImageButtonText = "Cambiar imagen";
                    ActualNameText = "Nombre actual";
                    ChangeNameButtonText = "Cambiar nombre";
                    ActualPasswordText = "Contraseña actual";
                    NewPasswordText = "Contraseña nueva";
                    ConfirmPasswordText = "Confirmar contraseña";
                    ChangePasswordButtonText = "Cambiar contraseña";
                }
                else
                {
                    EditUserTittle = "User configuration";
                    ChangeImageButtonText = "Change image";
                    ActualNameText = "Actual name";
                    ChangeNameButtonText = "Change name";
                    ActualPasswordText = "Actual password";
                    NewPasswordText = "New password";
                    ConfirmPasswordText = "Confirm password";
                    ChangePasswordButtonText = "Change password";
                }
            }
        }

        private void Update()
        {
            Name = service.ConsultUser().Name;
            Url = service.ConsultLocal().Url;
            LanguageSelected = service.ConsultLanguage().Name;
        }

        private async System.Threading.Tasks.Task ChangeName()
        {
            var user = service.ConsultUser();
            user.Name = Name;
            service.SaveUser(user);
            await Application.Current.MainPage.DisplayAlert("Felicidades", "Se ha cambiado el nombre satisfactoriamente satisfactoriamente", "Aceptar");
            (Application.Current).MainPage = new MainPage();
        }

        private async System.Threading.Tasks.Task ChangePassword()
        {
            if (string.IsNullOrEmpty(OldPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La primera contraseña no puede ser nula", "Aceptar");
            }
            else
            {
                if (OldPassword != service.ConsultUser().Password)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "La antigua contraseña no es válida", "Aceptar");
                } else
                {
                    if (string.IsNullOrEmpty(Password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "La segunda contraseña no puede ser nula", "Aceptar");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Password2))
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Ambas contraseñas deben ser iguales", "Aceptar");
                        }
                        else
                        {
                            var user = service.ConsultUser();
                            user.Password = Password;
                            service.SaveUser(user);
                            await Application.Current.MainPage.DisplayAlert("Felicidades", "Se ha cambiado la contraseña satisfactoriamente satisfactoriamente", "Aceptar");
                            (Application.Current).MainPage = new MainPage();
                        }
                    }
                }
            }
        }

    }

}
