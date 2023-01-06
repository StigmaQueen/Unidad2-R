using ClienteXamarinVuelos.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ClienteXamarinVuelos.Converters
{
    public class RemarkStatusStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int estado = (int)value;

            if (estado == (int)Estado.ATiempo)
                return "A Tiempo";
            if (estado == (int)Estado.Cancelado)
                return "Cancelado";
            if (estado == (int)Estado.Restrasado)
                return "Restrasado";
            if (estado == (int)Estado.Abordando)
                return "Abordando";
            if (estado == (int)Estado.Despegado)
                return "Despegado";

            return estado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
