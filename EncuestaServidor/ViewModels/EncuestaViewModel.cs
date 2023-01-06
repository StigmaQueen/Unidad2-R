using EncuestaServidor.Models;
using EncuestaServidor.Services;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EncuestaServidor.ViewModels
{
    public enum Vistas
    {
        Pregunta,
        Grafica
    }
    public class EncuestaViewModel : INotifyPropertyChanged
    {
        public Pregunta? Pregunta1 { get; set; }
        public Vistas Vista { get; set; }
        public ICommand IniciarCommand { get; set; }
        public int Voto1 { get; set; }
        public int Voto2 { get; set; }
        public int Voto3 { get; set; }
        public int Voto4 { get; set; }
        public int Voto5 { get; set; }
        public int Voto6 { get; set; }
        public int Voto7 { get; set; }
        public int Voto8 { get; set; }
        public int Voto9 { get; set; }
        public int Voto10 { get; set; }
        public int Voto11 { get; set; }
        public int Voto12 { get; set; }
        public string Error { get; set; } = "";
        public int Total => Voto1 + Voto2 + Voto3+ Voto4 + Voto5 + Voto6 + Voto7 + Voto8 + Voto9 + Voto10 + Voto12;

      

        EncuestaService server = new();

        List<int> Votos;

        public SeriesCollection PreguntaUno { get; set; } = new SeriesCollection();

        public EncuestaViewModel()
        {
            Vista = Vistas.Pregunta;
            IniciarCommand = new RelayCommand(Iniciar);
            server.VotoRecibido += Server_VotoRecibido;
            CargarPregunta();
            Votos= new();
            AcomodarPreguntas();
        }


        

        private void AcomodarPreguntas()
        {
           
            var vals = new ChartValues<ObservableValue>();
            vals.Add(new ObservableValue(Voto1));
            vals.Add(new ObservableValue(Voto2));
            vals.Add(new ObservableValue(Voto3));
            vals.Add(new ObservableValue(Voto4));


            PreguntaUno.Add(new PieSeries
            {
                Values = vals
            });

        }

        private void Server_VotoRecibido(Voto obj)
        {
            Votos.Clear();

            Votos.Add(obj.Opcion1);
            Votos.Add(obj.Opcion2);
            Votos.Add(obj.Opcion3);


            for (int i = 0; i < 3; i++)
            {
                var v = Votos[i];

                switch (v)
                {
                    case 1: Voto1++; ; break;
                    case 2: Voto2++; break;
                    case 3: Voto3++; break;
                    case 4: Voto4++; break;
                    case 5: Voto5++; break;
                    case 6: Voto6++; break;
                    case 7: Voto7++; break;
                    case 8: Voto8++; break;
                    case 9: Voto9++; break;
                    case 10: Voto10++; break;
                    case 11: Voto11++; break;
                    case 12: Voto12++; break;
                }

                Actualizar();
            }
           
        }

        private void Server_VotoRecibido1(int obj)
        {
            switch (obj)
            {
                case 1: Voto1++; break;
                case 2: Voto2++; break;
                case 3: Voto3++; break;
                case 4: Voto4++; break;
                case 5: Voto5++; break;
                case 6: Voto6++; break;
                case 7: Voto7++; break;
                case 8: Voto8++; break;
                case 9: Voto9++; break;
                case 10:Voto10++;break;
                case 11:Voto11++;break;
                case 12:Voto12++;break;

            }
            Actualizar();  
        }

        private void CargarPregunta()
        {
            if (File.Exists("pregunta.json"))
            {
                var contenido = File.ReadAllText("pregunta.json");
                Pregunta1 = JsonConvert.DeserializeObject<Pregunta>(contenido);
            }
            else
            {
                Pregunta1 = new();
            }
            Actualizar(nameof(Pregunta1));
        }


        private void Iniciar()
        {

            if (Pregunta1 != null)
            {
                Error = "";
                //Valido la pregunta
                if (string.IsNullOrWhiteSpace(Pregunta1.Descripcion))
                {
                    Error = "Escribe las preguntas 1.";
                }
                if (string.IsNullOrWhiteSpace(Pregunta1.Descripcion1))
                {
                    Error = "Escribe la pregunta 2";
                }
                if (string.IsNullOrWhiteSpace(Pregunta1.Descripcion2))
                {
                    Error = "Escribe la pregunta 2";
                }
               
                
                if (Error == "")
                {
                    server.EstablecerPregunta(Pregunta1);
                    server.Iniciar();
                    var json = JsonConvert.SerializeObject(Pregunta1);
                    File.WriteAllText("pregunta.json", json);
                    Vista = Vistas.Grafica;
                }
                Actualizar();
            }

        }

        void Actualizar(string? Property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
    