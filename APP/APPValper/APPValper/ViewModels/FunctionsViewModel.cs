using APPValper.Models;
using APPValper.Resources;
using APPValper.Services;
using APPValper.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace APPValper.ViewModels
{
    public class FunctionsViewModel : Crud
    {
        private ObservableCollection<Brand> BrandsLocal { get; set; }
        private Task<ObservableCollection<Brand>> BrandsTask { get; set; }
        private ObservableCollection<Brand> BrandsAux { get; set; }
        public ObservableCollection<Brand> Brands { get; set; }

        public string Url { get; private set; }

        private ObservableCollection<Car> CarsLocal { get; set; }
        private Task<ObservableCollection<Car>> CarsTask { get; set; }
        private ObservableCollection<Car> CarsAux { get; set; }
        public ObservableCollection<Car> Cars { get; set; }

        Brand BrandModel;
        Car CarModel;

        FunctionsService service = new FunctionsService();

        OptionsService service2 = new OptionsService();

        public Language Language { get; set; }

        private string LanguageSelected;
        private int BrandsID = 0;

        public string CarTitle { get; set; }
        public string BrandTitle { get; set; }
        public string BrandNameText { get; set; }
        public string HeadquartersText { get; set; }
        public string FounderText { get; set; }
        public string SaveText { get; set; }
        public string ModifyText { get; set; }
        public string DeleteText { get; set; }
        public string ClearText { get; set; }
        public string ModelTitle { get; set; }
        public string BrandPickerText { get; set; }
        public string ModelNameText { get; set; }
        public string PowerText { get; set; }
        public string ColorText { get; set; }
        public string DoorsText { get; set; }

        public Command SaveBrandCommand { get; set; }
        public Command ModifyBrandCommand { get; set; }
        public Command DeleteBrandCommand { get; set; }
        public Command CleanBrandCommand { get; set; }

        public Command SaveCarCommand { get; set; }
        public Command ModifyCarCommand { get; set; }
        public Command DeleteCarCommand { get; set; }
        public Command CleanCarCommand { get; set; }
        public Command ChangeBrandIDCommand { get; set; }

        public FunctionsViewModel()
        {
            Update();
            Brand();
            Car();
            CheckLanguage();
        }

        private void Brand()
        {
            ListViewBrand();
            ListViewAsyncBrand();
            SaveBrandCommand = new Command(async () => await SaveBrand(), () => !IsBusy);
            ModifyBrandCommand = new Command(async () => await ModifyBrand(), () => !IsBusy);
            DeleteBrandCommand = new Command(async () => await DeleteBrand(), () => !IsBusy);
            CleanBrandCommand = new Command(CleanBrand, () => !IsBusy);
        }

        private void Car()
        {
            ListViewCar();
            ListViewAsyncCar();
            SaveCarCommand = new Command(async () => await SaveCar(), () => !IsBusy);
            ModifyCarCommand = new Command(async () => await ModifyCar(), () => !IsBusy);
            DeleteCarCommand = new Command(async () => await DeleteCar(), () => !IsBusy);
            CleanCarCommand = new Command(CleanCar, () => !IsBusy);
            ChangeBrandIDCommand = new Command(ChangeBrandID, () => !IsBusy);
        }

        private void CheckLanguage()
        {
            if (!string.IsNullOrEmpty(LanguageSelected))
            {
                if (LanguageSelected.Equals("Spanish"))
                {
                    BrandTitle = "Marcas de coche";
                    BrandNameText = "Nombre de la marca";
                    HeadquartersText = "Sede";
                    FounderText = "Fundador";
                    SaveText = "Guardar";
                    ModifyText = "Modificar";
                    DeleteText = "Eliminar";
                    ClearText = "Limpiar";
                    ModelTitle = "Modelos de coche";
                    BrandPickerText = "Nombre de la marca";
                    ModelNameText = "Nombre del modelo";
                    PowerText = "Caballos de potencia";
                    ColorText = "Color";
                    DoorsText = "Numero de puertas";
                }
                else
                {
                    BrandTitle = "Brand's car";
                    BrandNameText = "Brand name";
                    HeadquartersText = "Headquarters";
                    FounderText = "Founder";
                    SaveText = "Save";
                    ModifyText = "Modify";
                    DeleteText = "Delete";
                    ClearText = "Clean";
                    ModelTitle = "Brand's Models";
                    BrandPickerText = "Brand name";
                    ModelNameText = "Model name";
                    PowerText = "power";
                    ColorText = "Color";
                    DoorsText = "Door's number";
                }
            }
        }

        private void Update()
        {
            Url = service2.ConsultLocal().Url;
            LanguageSelected = service2.ConsultLanguage().Name;
        }

        private void ListViewBrand()
        {
            Brands = new ObservableCollection<Brand>();
            BrandsLocal = new ObservableCollection<Brand>();
            BrandsAux = service.ConsultLocalBrand();
            for (int i = 0; i < BrandsAux.Count; i++)
            {
                BrandsLocal.Add(BrandsAux[i]);
            }
            Brands = BrandsLocal;
        }

        private async Task ListViewAsyncBrand()
        {
            BrandsTask = service.ConsultBrand();
            BrandsAux = await BrandsTask;
            SyncroLocalBrandAsync();
            SyncroBrand();
            if (Brands[0].Id != null)
            {
                Brands = new ObservableCollection<Brand>();
            }
            BrandsTask = service.ConsultBrand();
            BrandsAux = await BrandsTask;
            for (int i = 0; i < BrandsAux.Count; i++)
            {
                Console.WriteLine("prueba" + BrandsAux[i].Id);
                Brands.Add(BrandsAux[i]);
            }
        }

        private void SyncroBrand()
        {
            for (int i = 0; i < BrandsLocal.Count; i++)
            {
                service.SaveBrand(BrandsLocal[i]);
            }
        }

        private async Task SyncroLocalBrandAsync()
        {
            for (int i = 0; i < BrandsAux.Count; i++)
            {
                service.SaveLocalBrand(BrandsAux[i]);
            }
        }

        private async Task SaveBrand()
        {
            IsBusy = true;
            Guid idBrand = Guid.NewGuid();
            BrandModel = new Brand()
            {
                Name = Name,
                Headquarters = Headquarters,
                Founder = Founder,
                Id = idBrand.ToString()
            };
            if (string.IsNullOrEmpty(BrandModel.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El modelo no puede ser nulo", "Aceptar");
            }
            else
            {
                service.SaveLocalBrand(BrandModel);
                Brands.Add(BrandModel);
                CleanBrand();
                if (await service.CheckConnection())
                {
                    service.SaveBrand(BrandModel);
                }
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task ModifyBrand()
        {
            IsBusy = true;
            BrandModel = new Brand()
            {
                Name = Name,
                Headquarters = Headquarters,
                Founder = Founder,
                Id = BrandID
            };
            service.ModifyLocalBrand(BrandModel);
            var item = Brands.FirstOrDefault(i => i.Id == BrandModel.Id);
            if (item != null)
            {
                item.Name = BrandModel.Name;
                item.Headquarters = BrandModel.Headquarters;
                item.Founder = BrandModel.Founder;
                item.Id = BrandModel.Id;
            }
            CleanBrand();
            if (await service.CheckConnection())
            {
                service.ModifyBrand(BrandModel);
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task DeleteBrand()
        {
            IsBusy = true;
            BrandModel = new Brand()
            {
                Name = Name,
                Headquarters = Headquarters,
                Founder = Founder,
                Id = BrandID
            };
            service.DeleteLocalBrand(BrandModel);
            var item = Brands.FirstOrDefault(i => i.Id == BrandModel.Id);
            Brands.Remove(item);
            CleanBrand();
            if (await service.CheckConnection())
            {
                service.DeleteBrand(BrandModel.Id);
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private void CleanBrand()
        {
            SaveText = "Hola";
            Model = "";
            Power = "";
            Color = "";
            Ndoor = 0;
            IdCar = "";
            BrandID = "";
            Name = "";
            Headquarters = "";
            Founder = "";
        }

        private void ListViewCar()
        {
            Cars = new ObservableCollection<Car>();
            CarsLocal = new ObservableCollection<Car>();
            CarsAux = service.ConsultLocalCar();
            for (int i = 0; i < CarsAux.Count; i++)
            {
                CarsLocal.Add(CarsAux[i]);
            }
            Cars = CarsLocal;
        }

        private async Task ListViewAsyncCar()
        {
            CarsTask = service.ConsultCar();
            CarsAux = await CarsTask;
            SyncroLocalCarAsync();
            SyncroCar();
            if (Cars[0].Id != null)
            {
                Cars = new ObservableCollection<Car>();
            }
            CarsTask = service.ConsultCar();
            CarsAux = await CarsTask;
            for (int i = 0; i < CarsAux.Count; i++)
            {
                Cars.Add(CarsAux[i]);
            }
        }

        private void SyncroCar()
        {
            for (int i = 0; i < CarsLocal.Count; i++)
            {
                service.SaveCar(CarsLocal[i]);
            }
        }

        private async Task SyncroLocalCarAsync()
        {
            for (int i = 0; i < CarsAux.Count; i++)
            {
                service.SaveLocalCar(CarsAux[i]);
            }
        }

        private async Task SaveCar()
        {
            IsBusy = true;
            Guid idCar = Guid.NewGuid();
            CarModel = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                BrandID = BrandID,
                Id = idCar.ToString()
            };
            if (string.IsNullOrEmpty(CarModel.Model))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El modelo no puede ser nulo", "Aceptar");
            }
            else
            {
                service.SaveLocalCar(CarModel);
                Cars.Add(CarModel);
                CleanCar();
                if (await service.CheckConnection())
                {
                    service.SaveCar(CarModel);
                }
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task ModifyCar()
        {
            IsBusy = true;
            CarModel = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                BrandID = BrandID,
                Id = IdCar
            };
            service.ModifyLocalCar(CarModel);
            var item = Cars.FirstOrDefault(i => i.Id == CarModel.Id);
            if (item != null)
            {
                item.Model = CarModel.Model;
                item.Power = CarModel.Power;
                item.Color = CarModel.Color;
                item.Ndoor = CarModel.Ndoor;
                item.Id = CarModel.Id;
            }
            CleanCar();
            if (await service.CheckConnection())
            {
                service.ModifyCar(CarModel);
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task DeleteCar()
        {
            IsBusy = true;
            CarModel = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                BrandID = BrandID,
                Id = IdCar
            };
            service.DeleteLocalCar(CarModel);
            var item = Cars.FirstOrDefault(i => i.Id == CarModel.Id);
            Cars.Remove(item);
            CleanCar();
            if (await service.CheckConnection())
            {
                service.DeleteCar(CarModel.Id);
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private void CleanCar()
        {
            Model = "";
            Power = "";
            Color = "";
            Ndoor = 0;
            BrandID = "";
            IdCar = "";
            Name = "";
            Headquarters = "";
            Founder = "";
        }

        private void ChangeBrandID()
        {
            if (BrandID == Brands[BrandsID].Id)
            {
                if (BrandsID + 1 >= Brands.Count)
                {
                    BrandsID = 0;
                }
                else
                {
                    BrandsID++;
                }
                BrandID = Brands[BrandsID].Id;
            }
            else
            {
                BrandID = Brands[BrandsID].Id;
                if (BrandsID + 1 >= Brands.Count)
                {
                    BrandsID = 0;
                }
                else
                {
                    BrandsID++;
                }
            }

        }

    }
}
