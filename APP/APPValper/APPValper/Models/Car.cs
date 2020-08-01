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
    public partial class Car : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string model = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(model));
        }

        private string id;

        [PrimaryKey]
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
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
