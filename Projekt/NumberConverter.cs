using System.Globalization;
using System.Windows.Data;

namespace Projekt
{

    [ValueConversion(typeof(Double), typeof(String))]
    public class NumberConverter : IValueConverter
    {
        private static List<String> illions = ["", "K", "M", "B", "T", "Qa", "Qt", "Sx", "Sp", "O", "N", "D"];
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            int lg = Math.Max(0, (int)Math.Log10(val) / 3);
            if (lg < 12)
            {
                return string.Format("{0:0.##}{1}", val / Math.Pow(1000, lg), illions[lg]);
            }
            return string.Format("{0:0.###E-000}", val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
