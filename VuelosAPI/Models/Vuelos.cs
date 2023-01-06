using System;
using System.Collections.Generic;

namespace VuelosAPI.Models
{
    public partial class Vuelos
    {
        public int Id { get; set; }
        public int Puerta { get; set; }
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
