using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EmployeeDatabase
{
    class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!(value is bool v)) throw new ArgumentException();
            if (v) return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility v)) throw new ArgumentException();
            switch (v)
            {
                case Visibility.Visible: return true;
                case Visibility.Hidden:
                case Visibility.Collapsed: return false;
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
