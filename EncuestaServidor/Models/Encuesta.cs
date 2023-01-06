using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestaServidor.Models
{
    public class Encuesta
    {
        public List<Pregunta> Preguntas { get; set; }= new List<Pregunta>();    
        public string pregunta1 { get; set; } = "";
        public string pregunta2 { get; set; } = "";
        public string pregunta3 { get; set; } = "";
    }
}
