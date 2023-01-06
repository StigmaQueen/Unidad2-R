using ClienteAerolioneaWPF.Models;
using ClienteAerolioneaWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ClienteAerolioneaWPF.ViewModels
{
    public class AerolineaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<VueloDTO>? VeulosEntrantes { get; set; }
        public VueloDTO Vuelo { get; set; } = new();

        private DateTime hora = DateTime.Now;

        public DateTime Hora
        {
            get { return hora; }
            set { hora = value; Actualizar(); }
        }

        public DateTime HoraDosMinutos { get; set; } = DateTime.Now.AddSeconds(10);


        AerolineaService service = new AerolineaService();


        public AerolineaViewModel()
        {
            DispatcherTimer ActualizarVuelos = new DispatcherTimer();
            ActualizarVuelos.Interval = TimeSpan.FromSeconds(5);
            ActualizarVuelos.Tick += ActualizarVuelos_Tick;
            ActualizarVuelos.Start();
            VeulosEntrantes = new(service.Get().Result);

            DispatcherTimer ActualizarHora = new DispatcherTimer();

            ActualizarHora.Interval = TimeSpan.FromSeconds(1);
            ActualizarHora.Tick += ActualizarHora_Tick;
            ActualizarHora.Start();
        }

        private void ActualizarHora_Tick(object? sender, EventArgs e)
        {
            Hora = DateTime.Now;
            Actualizar(nameof(Hora));
        }

        private void ActualizarVuelos_Tick(object? sender, EventArgs e)
        {
            VeulosEntrantes = new(service.Get().Result);
            ////Primero debo recorrer uno por uno

            //for (int i = 0; i < VeulosEntrantes.Count(); i++)
            //{
            //    var vueloactual = VeulosEntrantes[i];

            //    if (vueloactual.Estado == Estado.ATiempo && vueloactual.Fecha < Hora)
            //    {
            //        vueloactual.Estado = Estado.Abordando;
            //        await service.Put(vueloactual);
            //        HoraDosMinutos = Hora.AddSeconds(10);
            //    }

            //    if (vueloactual.Estado == Estado.Abordando && HoraDosMinutos < Hora)
            //    {
            //        vueloactual.Estado = Estado.Despegado;
            //        await service.Put(vueloactual);
            //        HoraDosMinutos = Hora.AddSeconds(10);
            //    }

            //    if ((vueloactual.Estado == Estado.Cancelado || vueloactual.Estado == Estado.Despegado)
            //         && HoraDosMinutos < Hora)
            //    {
            //        await service.Delete(vueloactual);
            //        HoraDosMinutos = Hora.AddSeconds(10);
            //    }


            //}


            ////Evaluar la fecha de cada unio
            ////Si la fecha del vuelo es menor debo hacer un putasync
            ////Hacer un get de nuez
            ////Actualizo la vista

            //VeulosEntrantes = new(service.Get().Result);

            Actualizar();
        }

        void Actualizar(string? Property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
