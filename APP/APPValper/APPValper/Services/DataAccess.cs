using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.IO;
using SQLite;
using APPValper.Models;
using APPValper.Services;

namespace APPValper.Services
{
    class DataAccess : IDisposable
    {
        private SQLiteConnection connection;
         
        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(Path.Combine(config.DirDB, "ValperDB.db3"), false);
            //connection.CreateTable<Function>();
            connection.CreateTable<Connection>();
            connection.CreateTable<Language>();
            connection.CreateTable<Car>();
            connection.CreateTable<Brand>();
            connection.CreateTable<User>();
        }

        public void InsertCar(Car car)
        {
            connection.Insert(car);
        }

        public void ModifyCar(Car car)
        {
            connection.Update(car);
        }

        public void DeleteCar(Car car)
        {
            connection.Delete(car);
        }

        public Car GetCar(int IDCar)
        {
            return connection.Table<Car>().FirstOrDefault(c => c.Id.Equals(IDCar));
        }

        public List<Car> GetCars()
        {
            return connection.Table<Car>().OrderBy(c => c.Model).ToList();
        }

        public void InsertBrand(Brand brand)
        {
            connection.Insert(brand);
        }

        public void ModifyBrand(Brand brand)
        {
            connection.Update(brand);
        }

        public void DeleteBrand(Brand brand)
        {
            connection.Delete(brand);
        }

        public Brand GetBrand(int IDBrand)
        {
            return connection.Table<Brand>().FirstOrDefault(c => c.Id.Equals(IDBrand));
        }

        public List<Brand> GetBrands()
        {
            return connection.Table<Brand>().OrderBy(c => c.Name).ToList();
        }

        //public void InsertFunction(Function function)
        //{
        //    connection.Insert(function);
        //}

        //public void ModifyFunction(Function function)
        //{
        //    connection.Update(function);
        //}

        //public void DeleteFunction(Function function)
        //{
        //    connection.Delete(function);
        //}

        //public Function GetFunction(int IDFunction)
        //{
        //    return connection.Table<Function>().FirstOrDefault(c => c.Id.Equals(IDFunction));
        //}

        //public List<Function> GetFunctions()
        //{
        //    return connection.Table<Function>().OrderBy(c => c.Server).ToList();
        //}

        public Language GetLanguage()
        {
            if (connection.Table<Language>().ToList().Count > 0)
            {
                return connection.Table<Language>().FirstOrDefault(c => c.Id.Equals(0));
            }
            return new Language();
        }

        public void InsertLanguage(Language language)
        {
            if (connection.Table<Language>().ToList().Count > 0)
            {
                connection.Delete(connection.Table<Language>().FirstOrDefault(c => c.Id.Equals(0)));
            }
            connection.Insert(language);
        }

        public Connection GetConnection()
        {
            if (connection.Table<Connection>().ToList().Count > 0)
            {
                return connection.Table<Connection>().FirstOrDefault(c => c.Id.Equals(0));
            }
            return new Connection();
        }

        public void InsertConnection(Connection con)
        {
            if (connection.Table<Connection>().ToList().Count > 0)
            {
                connection.Delete(connection.Table<Connection>().FirstOrDefault(c => c.Id.Equals(0)));
            }
            connection.Insert(con);
        }

        public void InsertUser(User user)
        {
            if (connection.Table<User>().ToList().Count > 0)
            {
                connection.Delete(connection.Table<User>().FirstOrDefault(c => c.Id.Equals(0)));
            }
            connection.Insert(user);
        }

        public void ModifyUser(User user)
        {
            connection.Update(user);
        }

        public void DeleteUser(User user)
        {
            connection.Delete(user);
        }

        public User GetUser()
        {
            return connection.Table<User>().FirstOrDefault(c => c.Id.Equals(0));
        }

        public List<User> GetUsers()
        {
            return connection.Table<User>().OrderBy(c => c.Name).ToList();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
