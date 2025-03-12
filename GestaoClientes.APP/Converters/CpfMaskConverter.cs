using System.Globalization;
using System.Windows.Data;

namespace GestaoClientes.APP.Converters;

public class CpfMaskConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string cpf = value.ToString();

        if (!string.IsNullOrEmpty(cpf) && cpf.Length == 11)
        {
            return string.Format("{0:000\\.000\\.000-00}", double.Parse(cpf));
        }

        return cpf;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
