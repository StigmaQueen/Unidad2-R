using ClienteAerolioneaWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClienteAerolioneaWPF.Services
{
    public class AerolineaService
    {
        HttpClient client;

        public AerolineaService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://cinnamorollareolines.sistemas19.com/api/Vuelos");
        }
        public async Task<List<VueloDTO>> Get()
        {
            List<VueloDTO>? vuelos = new List<VueloDTO>();
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

        public async Task Put(VueloDTO vuelo)
        {

            var json = JsonConvert.SerializeObject(vuelo);
            var response = client.PutAsync("", new StringContent(json, Encoding.UTF8,
                "application/json"));
            response.Wait();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Result.Content.ReadAsStringAsync();
            }
        }

        public async Task Delete(VueloDTO vuelo)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(vuelo), Encoding.UTF8,
                                "application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete,"");
            request.Content = stringContent;
            var response = client.SendAsync(request);
            response.Wait();
            if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Result.Content.ReadAsStringAsync();
            }
        }

    }
}
