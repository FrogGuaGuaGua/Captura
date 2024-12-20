﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Captura
{
    public class TimeSpanToSecondsConverter : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is TimeSpan t)
                return t.TotalSeconds;

            return Binding.DoNothing;
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is double seconds)
                return TimeSpan.FromSeconds(seconds);

            return Binding.DoNothing;
        }
    }
}
