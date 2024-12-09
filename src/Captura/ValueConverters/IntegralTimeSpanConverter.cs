using System;
using System.Globalization;
using System.Windows.Data;

namespace Captura
{
    public class IntegralTimeSpanConverter : OneWayConverter
    {
        public override object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is TimeSpan t)
                return TimeSpan.FromSeconds((int)t.TotalSeconds);

            return Binding.DoNothing;
        }
    }
}
