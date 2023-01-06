using EncuestaServidor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using EncuestaCliente.Services;
using Xamarin.Forms;

namespace EncuestaCliente.ViewModels
{
    public class EncuestaViewModel : INotifyPropertyChanged
    {
        EncuestaVCliente cliente= new EncuestaVCliente();

        public Pregunta Pregunta { get; set; }
        public string Error { get; set; }
        public Command VotarCommand { get; set; }

        public bool Votado { get; set; }

        public EncuestaViewModel()
        {
            VotarCommand = new Command<Voto>(Votar);

            CargarPreguntas();
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
                    if (v.Opcion1 >=1&& v.Opcion1<=4)
                    {
                       C.Opcion1= v.Opcion1; 
                    }
                    if(v.Opcion2>=5 && v.Opcion2<=8)
                    {
                      C.Opcion2= v.Opcion2;
                    }    
                    if(v.Opcion3>=8 && v.Opcion3 <= 12)
                    {
                       C.Opcion3= v.Opcion3;
                    }
                   
                   
                    
                    await cliente.VotarPost(C);
                    Votado = false;
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
