using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EncuestaServidor.ViewModels;
using LiveCharts;
using LiveCharts.Wpf;

namespace EncuestaServidor.Views
{
    /// <summary>
    /// Lógica de interacción para ResultadosView.xaml
    /// </summary>
    public partial class ResultadosView : UserControl
    {
        public ResultadosView()
        {
            InitializeComponent();
         
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
