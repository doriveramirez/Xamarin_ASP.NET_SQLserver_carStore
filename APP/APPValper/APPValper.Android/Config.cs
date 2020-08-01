using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using APPValper.Services;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppXamarinCrud.Droid.Config))]

namespace AppXamarinCrud.Droid
{
    class Config : IConfig
    {

        private string dirDB;
        private ISQLitePlatform platform;

        public string DirDB
        {
            get
            {
                if (string.IsNullOrEmpty(dirDB))
                {
                    dirDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return dirDB;
            }
        }

        public ISQLitePlatform Platform
        { 
            get
            {
                if (platform == null)
                {
                    //platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return platform;
            }
        }
    }
}