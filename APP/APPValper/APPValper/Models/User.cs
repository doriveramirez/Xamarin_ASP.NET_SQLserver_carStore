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
    public partial class User : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string model = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(model));
        }

        private int id;

        [PrimaryKey]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged();
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value;
                OnPropertyChanged();
            }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set { password = value;
                OnPropertyChanged();
            }
        }

        private bool logged;

        public bool Logged
        {
            get { return logged; }
            set { logged = value;
                OnPropertyChanged();
            }
        }

        private bool admin;

        public bool Admin
        {
            get { return admin; }
            set { admin = value;
                OnPropertyChanged();
            }
        }

    }
}
