using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteAerolioneaWPF.Models
{

    public enum Estado
    {
        ATiempo = 1,
        Cancelado = 2,
        Restrasado = 3,
        Abordando = 4,
        Despegado = 5
    }

    public class VueloDTO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        public int Puerta { get; set; }
        public string Origen { get; set; } = "";
        public string Destino { get; set; } = "";
        public string Clave { get; set; } = "";
        public Estado Estado { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

}
