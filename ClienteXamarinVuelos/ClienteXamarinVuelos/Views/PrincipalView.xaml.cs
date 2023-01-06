using ClienteXamarinVuelos.Models;
using ClienteXamarinVuelos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClienteXamarinVuelos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalView : ContentPage
    {
        public PrincipalView()
        {
            
            InitializeComponent();
            lstFlights.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
            {
                ItemSpacing = 20
            };
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var viewmodel = (PrincipalViewModel)this.BindingContext;

            var vuelo = ((ImageButton)sender).BindingContext;

            viewmodel.VerEditarCommand.Execute(vuelo);
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            var viewmodel = (PrincipalViewModel)this.BindingContext;

            VueloDTO vuelo = (VueloDTO)((ImageButton)sender).BindingContext;

            bool respuesta = await Application.Current.MainPage.DisplayAlert("ADVERTENCIA", $"¿Deseas eliminar el vuelo de {vuelo.Origen} " +
                $"hacia {vuelo.Destino} con la clave {vuelo.Clave}", "Si", "No");

            if (respuesta)
                viewmodel.EliminarCommand.Execute(vuelo);
        }
    }
}