using APPValper.Models;
using APPValper.Services;
using APPValper.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace APPValper.ViewModels
{
    class RegisterViewModel : Connection
    {
        OptionsService service = new OptionsService();
        public Command RegisterCommand { get; set; }
        public Command AdminCommand { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        INavigation Navigation;
        private bool admin = false;

        private string LanguageSelected;
        public Language Language { get; set; }
        public string EmailText { get; set; }
        public string FullNameText { get; set; }
        public string PasswordText { get; set; }
        public string ConfirmPasswordText { get; set; }
        public string RegisterText { get; set; }
        public string AdmindText { get; set; }

        public RegisterViewModel(INavigation Nav)
        {
            Update();
            Navigation = Nav;
            RegisterCommand = new Command(async () => await Register(), () => !IsBusy);
            AdminCommand = new Command(async () => await Admin(), () => !IsBusy);
            CheckLanguage();
        }

        private void CheckLanguage()
        {
            if (!string.IsNullOrEmpty(LanguageSelected))
            {
                if (LanguageSelected.Equals("Spanish"))
                {
                    EmailText = "Correo electronico";
                    FullNameText = "Nombre completo";
                    PasswordText = "Contraseña";
                    ConfirmPasswordText = "Confirme la contraseña";
                    RegisterText = "Registrarse";
                    AdmindText = "¿Quieres ser administrador?";
                }
                else
                {
                    EmailText = "E-mail";
                    FullNameText = "Full name";
                    PasswordText = "Password";
                    ConfirmPasswordText = "Confirm password";
                    RegisterText = "Register";
                    AdmindText = "Wanna be admin?";
                }
            }
        }

        private void Update()
        {
            Url = service.ConsultLocal().Url;
            LanguageSelected = service.ConsultLanguage().Name;
        }

        private async Task Admin()
        {
            if (!admin)
            {
                await Application.Current.MainPage.DisplayAlert("Felicidades", "Serás administrador.", "Aceptar");
                admin = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Ya no serás administrador.", "Aceptar");
                admin = false;
            }
        }

        private async System.Threading.Tasks.Task Register()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El email no puede ser nulo", "Aceptar");
            }
            else
            {
                if (string.IsNullOrEmpty(Username))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El nombre no puede ser nulo", "Aceptar");
                }
                else
                {
                    if (string.IsNullOrEmpty(Password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "La primera contraseña no puede ser nulo", "Aceptar");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Password2))
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "La segunda contraseña no puede ser nulo", "Aceptar");
                        }
                        else
                        {
                            if (!Password.Equals(Password2))
                            {
                                await Application.Current.MainPage.DisplayAlert("Error", "Ambas contraseñas deben ser iguales", "Aceptar");
                            }
                            else
                            {
                                var user = new User();
                                user.Id = 0;
                                user.Email = Email;
                                user.Name = Username;
                                user.Password = Password;
                                user.Logged = false;
                                service.SaveUser(user);
                                await Application.Current.MainPage.DisplayAlert("Felicidades", "Se ha registrado satisfactoriamente", "Aceptar");
                                (Application.Current).MainPage = new MainPage();
                            }
                        }
                    }
                }
            }
        }
    }
}
