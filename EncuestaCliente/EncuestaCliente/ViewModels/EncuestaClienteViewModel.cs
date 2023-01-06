using EncuestaCliente.Services;
using EncuestaServidor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace EncuestaCliente.ViewModels
{
    public class EncuestaClienteViewModel : INotifyPropertyChanged
    {
        EncuestaVCliente cliente = new EncuestaVCliente();

        public Pregunta Pregunta { get; set; }
        public string Error { get; set; }
        public Command VotarCommand { get; set; }
        public Command ElegirOpcionCommand { get; set; }
        public Voto Voto { get; set; }

        public bool Votado1 { get; set; } = true;
        public bool Votado2 { get; set; } = true;
        public bool Votado3 { get; set; } = true;

        public EncuestaClienteViewModel()
        {
            VotarCommand = new Command<Voto>(Votar);
            ElegirOpcionCommand = new Command<string>(ElegirOpcion);
            Voto = new Voto();

            CargarPreguntas();
        }

        private void ElegirOpcion(string opcion)
        {
            var opcionynumeropregunta = opcion.Split(',');
            var tipoR = opcionynumeropregunta[0];
            var numeroR = int.Parse(opcionynumeropregunta[1]);

            if (numeroR == 1)
            {
                switch (tipoR)
                {
                    case "Excelente": Voto.Opcion1 = 1; break;
                    case "Bueno": Voto.Opcion1 = 2; break;
                    case "Regular": Voto.Opcion1 = 3; break;
                    case "Malo": Voto.Opcion1 = 4; break;
                }
                Votado1 = false;
            }

            if (numeroR == 2)
            {
                switch (tipoR)
                {
                    case "Excelente": Voto.Opcion2 = 5; break;
                    case "Bueno": Voto.Opcion2 = 6; break;
                    case "Regular": Voto.Opcion2 = 7; break;
                    case "Malo": Voto.Opcion2 = 8; break;
                }
                Votado2 = false;
            }

            if (numeroR == 3)
            {
                switch (tipoR)
                {
                    case "Excelente": Voto.Opcion3 = 9; break;
                    case "Bueno": Voto.Opcion3 = 10; break;
                    case "Regular": Voto.Opcion3 = 11; break;
                    case "Malo": Voto.Opcion3 = 12; break;
                }
                Votado3 = false;
            }

            var actualizarvoto = "Votado" + numeroR;
            Lanzar(actualizarvoto);
        }

        private async void CargarPreguntas()
        {
            Pregunta = await cliente.GetPreguntas();

            if (Pregunta == null)
                Error = "No se pudo conectar al servidor";

            Lanzar();
        }


        private async void Votar(Voto v)
        {
            try
            {
                if (Pregunta != null)
                {
                    Voto C = new Voto();
                    if (v.Opcion1 >= 1 && v.Opcion1 <= 4)
                    {
                        C.Opcion1 = v.Opcion1;
                    }
                    if (v.Opcion2 >= 5 && v.Opcion2 <= 8)
                    {
                        C.Opcion2 = v.Opcion2;
                    }
                    if (v.Opcion3 >= 8 && v.Opcion3 <= 12)
                    {
                        C.Opcion3 = v.Opcion3;
                    }



                    await cliente.VotarPost(C);
                    Votado1 = false;
                    Lanzar();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Lanzar(nameof(Error));
            }

        }




        public void Lanzar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
