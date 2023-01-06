using CeciliaBrionesFlights.Views;
using ClienteXamarinVuelos.Models;
using ClienteXamarinVuelos.Services;
using ClienteXamarinVuelos.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ClienteXamarinVuelos.ViewModels
{
    public class PrincipalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command VerAddFlyCommand { get; set; }
        public Command AddFlyCommand { get; set; }
        public Command VerEditarCommand { get; set; }
        public Command EditarCommand { get; set; }
        public Command EliminarCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public ObservableCollection<VueloDTO> VuelosEntrantes { get; set; }
        public VueloDTO Vuelo { get; set; } = new VueloDTO();
        public TimeSpan HoraVuelo { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Estado { get; set; }
        public VueloDTO Clon { get; set; }
        public bool IsRefreshing { get; set; } = false;

        Service service = new Service();

        FlightModalPage addview;
        EditarView editview;

        public PrincipalViewModel()
        {
            VerAddFlyCommand = new Command(VerAddFly);
            AddFlyCommand = new Command(AddFly);
            VerEditarCommand = new Command<VueloDTO>(VerEditar);
            EditarCommand = new Command(Editar);
            EliminarCommand = new Command<VueloDTO>(Eliminar);
            RefreshCommand = new Command(Refresh);


            addview = new FlightModalPage() { BindingContext = this };
            editview = new EditarView() { BindingContext = this };

            VuelosEntrantes = new ObservableCollection<VueloDTO>(service.Get().Result);
            Actualizar();

        }

        private void Refresh()
        {
            IsRefreshing = true;
            VuelosEntrantes = new ObservableCollection<VueloDTO>(service.Get().Result);
            IsRefreshing = false;
            Actualizar();
        }

        private async void Eliminar(VueloDTO obj)
        {
            if (obj != null)
            {
                await service.Delete(obj);
                VuelosEntrantes = new ObservableCollection<VueloDTO>(service.Get().Result);
                Actualizar();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async void Editar(object obj)
        {

            //Validar
            //El estado no debe estar nullo
           
            
            if (Clon != null)
            {
                switch (Estado)
                {
                    case "A Tiempo": Clon.Estado = Models.Estado.ATiempo; break;
                    case "Retrasado": Clon.Estado = Models.Estado.Restrasado; break;
                    case "Cancelado": Clon.Estado = Models.Estado.Cancelado; break;
                    case "Depegado": Clon.Estado = Models.Estado.Despegado; break;
                    case "Abordado": Clon.Estado = Models.Estado.Abordando; break;
                    default:
                        break;
                }

                Clon.Fecha = Clon.Fecha.Date;
                Clon.Fecha = Clon.Fecha.Add(HoraVuelo);
                bool a= service.Update(Clon).Result;
                VuelosEntrantes = new ObservableCollection<VueloDTO>(service.Get().Result);
                Actualizar();
                await App.Current.MainPage.Navigation.PopAsync();

            }
        }

        private async void VerEditar(VueloDTO obj)
        {
            Clon = new VueloDTO()
            {
                Id = obj.Id,
                Destino = obj.Destino,
                Clave = obj.Clave,
                Estado = obj.Estado,
                Fecha = obj.Fecha,
                Origen = obj.Origen,
                Puerta = obj.Puerta
            };

            Estado = Clon.Estado.ToString();
            HoraVuelo = Clon.Fecha.TimeOfDay;
            Actualizar();
            await Application.Current.MainPage.Navigation.PushAsync(editview);

        }

        private async void AddFly()
        {
            //Validar
            if (Vuelo != null)
            {
                switch (Estado)
                {
                    case "A Tiempo": Vuelo.Estado = Models.Estado.ATiempo; break;
                    case "Retrasado": Vuelo.Estado = Models.Estado.Restrasado; break;
                    case "Cancelado": Vuelo.Estado = Models.Estado.Cancelado; break;
                    case "Depegado": Vuelo.Estado = Models.Estado.Despegado; break;
                    case "Abordado": Vuelo.Estado = Models.Estado.Abordando; break;
                    default:
                        break;
                }

                Vuelo.Fecha = Vuelo.Fecha.Date;
                Vuelo.Fecha = Vuelo.Fecha.Add(HoraVuelo);
                service.Post(Vuelo);
                VuelosEntrantes = new ObservableCollection<VueloDTO>(service.Get().Result);
                Actualizar();
                await App.Current.MainPage.Navigation.PopAsync();

            }
        }

        private async void VerAddFly()
        {
            Vuelo = new VueloDTO();
            Vuelo.Fecha = DateTime.Now;
            Actualizar(nameof(Vuelo));
            await Application.Current.MainPage.Navigation.PushAsync(addview);
        }

        void Actualizar(string Property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }

    }

}
