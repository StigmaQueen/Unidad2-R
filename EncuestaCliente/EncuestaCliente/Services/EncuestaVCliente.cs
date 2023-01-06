using EncuestaServidor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EncuestaCliente.Services
{
    internal class EncuestaVCliente
    {
        HttpClient cliente = new HttpClient();

        public EncuestaVCliente()
        {
            cliente.BaseAddress = new Uri("https://3a0b-2806-108e-26-4326-f422-3465-5893-5706.ngrok.io/votacion/");


        }

        public async Task<Pregunta> GetPreguntas()
        {
            HttpResponseMessage responseMessage = await cliente.GetAsync("preguntas");
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();
                Pregunta p = JsonConvert.DeserializeObject<Pregunta>(response);
                return p;
            }
            else
            {
                return null;
            }
        }

        public async Task Votar(int opcion)
        {
            var response = await cliente.GetAsync("responder?voto=" + opcion);
            response.EnsureSuccessStatusCode();
        }

        public async Task VotarPost(Voto voto)
        {
            var json = JsonConvert.SerializeObject(voto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("responder", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
