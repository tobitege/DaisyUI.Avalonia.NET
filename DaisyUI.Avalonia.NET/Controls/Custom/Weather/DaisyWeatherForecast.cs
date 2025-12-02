using System;
using Avalonia;
using Avalonia.Controls;

namespace DaisyUI.Avalonia.Controls.Custom.Weather
{
    /// <summary>
    /// Displays a horizontal strip of daily weather forecasts.
    /// </summary>
    public class DaisyWeatherForecast : ItemsControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyWeatherForecast);

        public static readonly StyledProperty<string> TemperatureUnitProperty =
            AvaloniaProperty.Register<DaisyWeatherForecast, string>(nameof(TemperatureUnit), "C");

        /// <summary>
        /// Temperature unit (C or F).
        /// </summary>
        public string TemperatureUnit
        {
            get => GetValue(TemperatureUnitProperty);
            set => SetValue(TemperatureUnitProperty, value);
        }

        public static readonly StyledProperty<bool> ShowPrecipitationProperty =
            AvaloniaProperty.Register<DaisyWeatherForecast, bool>(nameof(ShowPrecipitation), false);

        /// <summary>
        /// Whether to show precipitation chance.
        /// </summary>
        public bool ShowPrecipitation
        {
            get => GetValue(ShowPrecipitationProperty);
            set => SetValue(ShowPrecipitationProperty, value);
        }
    }
}
