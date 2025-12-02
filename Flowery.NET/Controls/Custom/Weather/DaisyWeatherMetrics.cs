using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls.Custom.Weather
{
    /// <summary>
    /// Displays weather metrics in a table format (UV, wind, humidity).
    /// </summary>
    public class DaisyWeatherMetrics : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyWeatherMetrics);

        public static readonly StyledProperty<double> UvIndexProperty =
            AvaloniaProperty.Register<DaisyWeatherMetrics, double>(nameof(UvIndex));

        /// <summary>
        /// Current UV index.
        /// </summary>
        public double UvIndex
        {
            get => GetValue(UvIndexProperty);
            set => SetValue(UvIndexProperty, value);
        }

        public static readonly StyledProperty<double> UvMaxProperty =
            AvaloniaProperty.Register<DaisyWeatherMetrics, double>(nameof(UvMax));

        /// <summary>
        /// Maximum UV index for the day.
        /// </summary>
        public double UvMax
        {
            get => GetValue(UvMaxProperty);
            set => SetValue(UvMaxProperty, value);
        }

        public static readonly StyledProperty<double> WindSpeedProperty =
            AvaloniaProperty.Register<DaisyWeatherMetrics, double>(nameof(WindSpeed));

        /// <summary>
        /// Current wind speed.
        /// </summary>
        public double WindSpeed
        {
            get => GetValue(WindSpeedProperty);
            set => SetValue(WindSpeedProperty, value);
        }

        public static readonly StyledProperty<double> WindMaxProperty =
            AvaloniaProperty.Register<DaisyWeatherMetrics, double>(nameof(WindMax));

        /// <summary>
        /// Maximum wind speed for the day.
        /// </summary>
        public double WindMax
        {
            get => GetValue(WindMaxProperty);
            set => SetValue(WindMaxProperty, value);
        }

        public static readonly StyledProperty<string> WindUnitProperty =
            AvaloniaProperty.Register<DaisyWeatherMetrics, string>(nameof(WindUnit), "km/h");

        /// <summary>
        /// Wind speed unit (e.g., "km/h", "mph").
        /// </summary>
        public string WindUnit
        {
            get => GetValue(WindUnitProperty);
            set => SetValue(WindUnitProperty, value);
        }

        public static readonly StyledProperty<int> HumidityProperty =
            AvaloniaProperty.Register<DaisyWeatherMetrics, int>(nameof(Humidity));

        /// <summary>
        /// Current humidity percentage.
        /// </summary>
        public int Humidity
        {
            get => GetValue(HumidityProperty);
            set => SetValue(HumidityProperty, value);
        }

        public static readonly StyledProperty<int> HumidityMaxProperty =
            AvaloniaProperty.Register<DaisyWeatherMetrics, int>(nameof(HumidityMax));

        /// <summary>
        /// Maximum humidity for the day.
        /// </summary>
        public int HumidityMax
        {
            get => GetValue(HumidityMaxProperty);
            set => SetValue(HumidityMaxProperty, value);
        }
    }
}
