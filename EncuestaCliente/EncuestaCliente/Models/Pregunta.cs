using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestaServidor.Models
{
    public class Pregunta
    {
        public string Descripcion { get; set; } = "";
        public string Descripcion1 { get; set; } = "";
        public string Descripcion2 { get; set; } = "";
        public string Excelente { get; set; } = "Excelente";
        public string Bueno { get; set; } = "Bueno";
        public string Regular { get; set; } = "Regular";
        public string Malo { get; set; } = "Malo";

    }
}
