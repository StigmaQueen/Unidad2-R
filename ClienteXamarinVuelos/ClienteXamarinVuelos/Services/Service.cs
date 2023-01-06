using ClienteXamarinVuelos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClienteXamarinVuelos.Services
{
    public class Service
    {
        HttpClient client;


        public Service()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://cinnamorollareolines.sistemas19.com/api/Vuelos");
        }
        public async Task<List<VueloDTO>> Get()
        {
            List<VueloDTO> vuelos = new List<VueloDTO>();
            var response = client.GetAsync("");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                var json = await response.Result.Content.ReadAsStringAsync();
                vuelos = JsonConvert.DeserializeObject<List<VueloDTO>>(json);
            }

            if (vuelos == null)
                return new List<VueloDTO>(); 
            else
                return vuelos;

        }

        public async Task Post(VueloDTO vuelo)
        {
            vuelo.Fecha = vuelo.Fecha.AddHours(2);
            var json = JsonConvert.SerializeObject(vuelo);
            var response = client.PostAsync("", new StringContent(json, Encoding.UTF8,
                "application/json"));
            response.Wait();
            if (response.Result.StatusCode == HttpStatusCode.BadRequest)
            {
                var errores = response.Result.Content.ReadAsStringAsync().Result;
                var myList = JsonConvert.DeserializeObject<List<string>>(errores);

                var e = String.Join(", ", myList);
                await App.Current.MainPage.DisplayAlert("Error", e, "OK");
            }
        }

        public async Task<bool> Update(VueloDTO vuelo)
        {

            var json = JsonConvert.SerializeObject(vuelo);
            var response =  client.PutAsync("", new StringContent(json, Encoding.UTF8,
                "application/json"));
            response.Wait();
            if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = response.Result.Content.ReadAsStringAsync().Result;
                var myList = JsonConvert.DeserializeObject<List<string>>(errores);

                var e = String.Join(", ", myList);
                App.Current.MainPage.DisplayAlert("Error", e, "OK");
                return false;
            }

            var message = response.Result.Content.ReadAsStringAsync();
            return true;
        }

        public async Task Delete(VueloDTO vuelo)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(vuelo), Encoding.UTF8,
                                "application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete, "");
            request.Content = stringContent;
            var response = client.SendAsync(request);
            response.Wait();
            if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = response.Result.Content.ReadAsStringAsync().Result;
                App.Current.MainPage.DisplayAlert("Error", errores, "OK");
            }
        }
    }
}
