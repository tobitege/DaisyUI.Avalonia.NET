using System;
using Avalonia;
using Avalonia.Data.Converters;

namespace Flowery.Controls
{
    public static class IndicatorConverters
    {
        public static readonly IValueConverter HalfConverter = new FuncValueConverter<double, double>(val => val / 2.0);
        public static readonly IValueConverter NegativeHalfConverter = new FuncValueConverter<double, double>(val => -val / 2.0);
        public static readonly IValueConverter HalfToCornerRadiusConverter = new FuncValueConverter<double, CornerRadius>(val => new CornerRadius(val / 2.0));
    }
}
