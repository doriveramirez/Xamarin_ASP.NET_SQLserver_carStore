using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace APPValper.Models
{
    public partial class Crud : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string idCar;

        public string IdCar
        {
            get { return idCar; }
            set
            {
                idCar = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string headquarters;

        public string Headquarters
        {
            get { return headquarters; }
            set
            {
                headquarters = value;
                OnPropertyChanged();
            }
        }

        private string founder;

        public string Founder
        {
            get { return founder; }
            set
            {
                founder = value;
                OnPropertyChanged();
            }
        }

        private string brandID;

        public string BrandID
        {
            get { return brandID; }
            set
            {
                brandID = value;
                OnPropertyChanged();
            }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }

        private string power;

        public string Power
        {
            get { return power; }
            set
            {
                power = value;
                OnPropertyChanged();
            }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }

        private int ndoor;

        public int Ndoor
        {
            get { return ndoor; }
            set
            {
                ndoor = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy = false; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}
