using APPValper.Services;
using APPValper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace APPValper.Resources
{
    public class FunctionsService
    {
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Brand> Brands { get; set; }
        private string apiUrl;
        private string apiUrl3;

        public FunctionsService()
        {
            using (var data = new DataAccess())
            {
                apiUrl = data.GetConnection().Url + "/api/Brands";
            }
            using (var data = new DataAccess())
            {
                apiUrl3 = data.GetConnection().Url + "/api/Cars";
            }
            if (Cars == null)
            {
                Cars = new ObservableCollection<Car>();
            }
            if (Brands == null)
            {
                Brands = new ObservableCollection<Brand>();
            }
        }

        public async System.Threading.Tasks.Task<ObservableCollection<Brand>> ConsultBrand()
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Brand");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        Brands = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(result);
                    }
                }
                return Brands;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async System.Threading.Tasks.Task<bool> CheckConnection()
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Brand");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ObservableCollection<Brand> ConsultLocalBrand()
        {
            using (var data = new DataAccess())
            {
                var list = data.GetBrands();
                foreach (var item in list)
                    Brands.Add(item);
            }
            return Brands;
        }

        public async void SaveBrand(Brand model)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Brand");
                    var send = Newtonsoft.Json.JsonConvert.SerializeObject(model,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
                    request.Content = new StringContent(send, Encoding.UTF8, "application/json");//CONTENT-TYPE header
                    HttpResponseMessage response = await client.SendAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveLocalBrand(Brand model)
        {
            using (var data = new DataAccess())
            {
                data.InsertBrand(model);
            }
        }

        public async void ModifyBrand(Brand model)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Brand");
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    Uri apiUrl2 = new Uri(string.Format(apiUrl + "/{0}", model.Id));
                    HttpResponseMessage response = await client.PutAsync(apiUrl2, content);
                    Console.WriteLine(response.IsSuccessStatusCode);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifyLocalBrand(Brand model)
        {
            using (var data = new DataAccess())
            {
                data.ModifyBrand(model);
            }
        }

        public async void DeleteBrand(string idBrand)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Brand");
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + idBrand);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteLocalBrand(Brand model)
        {
            using (var data = new DataAccess())
            {
                data.DeleteBrand(model);
            }
        }

        public async System.Threading.Tasks.Task<ObservableCollection<Car>> ConsultCar()
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Car");
                    HttpResponseMessage response = await client.GetAsync(apiUrl3);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        Cars = JsonConvert.DeserializeObject<ObservableCollection<Car>>(result);
                    }
                }
                return Cars;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ObservableCollection<Car> ConsultLocalCar()
        {
            using (var data = new DataAccess())
            {
                var list = data.GetCars();
                foreach (var item in list)
                    Cars.Add(item);
            }
            return Cars;
        }

        public async void SaveCar(Car model)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Car");
                    var send = Newtonsoft.Json.JsonConvert.SerializeObject(model,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
                    request.Content = new StringContent(send, Encoding.UTF8, "application/json");//CONTENT-TYPE header
                    HttpResponseMessage response = await client.SendAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveLocalCar(Car model)
        {
            using (var data = new DataAccess())
            {
                data.InsertCar(model);
            }
        }

        public async void ModifyCar(Car model)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Car");
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    Uri apiUrl2 = new Uri(string.Format(apiUrl3 + "/{0}", model.Id));
                    HttpResponseMessage response = await client.PutAsync(apiUrl2, content);
                    Console.WriteLine(response.IsSuccessStatusCode);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifyLocalCar(Car model)
        {
            using (var data = new DataAccess())
            {
                data.ModifyCar(model);
            }
        }

        public async void DeleteCar(string idCar)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient("Car");
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl3 + "/" + idCar);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteLocalCar(Car model)
        {
            using (var data = new DataAccess())
            {
                data.DeleteCar(model);
            }
        }

        private HttpClient CreateClient(String type)
        {
            HttpClient client = new HttpClient();
            if (type.Equals("Brand"))
            {
                client.BaseAddress = new Uri(apiUrl);
            }
            else
            {
                client.BaseAddress = new Uri(apiUrl3);
            }
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["token"].ToString());
            client.Timeout = TimeSpan.FromMinutes(10);
            client.Timeout = new TimeSpan(0, 0, 0, 0, -1);
            return client;
        }

    }
}