using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APPValper.Services;
using Foundation;
using SQLite.Net.Interop;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppXamarinCrud.iOS.Config))]

namespace AppXamarinCrud.iOS
{
    class Config : IConfig
    {

        private string dirDB;

        public string DirDB
        {
            get
            {
                if (string.IsNullOrEmpty(dirDB))
                {
                    var dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    dirDB = System.IO.Path.Combine(dir, "..", "Library");
                }
                return dirDB;
            }
        }

    }
}