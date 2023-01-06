using EncuestaCliente.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EncuestaCliente
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new EncuestaView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
