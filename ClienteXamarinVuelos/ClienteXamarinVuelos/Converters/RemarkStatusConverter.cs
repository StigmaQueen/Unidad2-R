using ClienteXamarinVuelos.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ClienteXamarinVuelos.Converters
{
    public class RemarkStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Estado estado = (Estado)value;

            if (estado == Estado.ATiempo)
                return "#66BB6A";
            if (estado == Estado.Cancelado)
                return "#AF0000";
            if (estado == Estado.Restrasado)
                return "#FF9E0B"; ;
            if (estado == Estado.Abordando)
                return "#f1c232";

            return "#0b5394";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
