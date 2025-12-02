using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace Flowery.Controls
{
    public class DaisyRadialProgress : RangeBase
    {
        protected override Type StyleKeyOverride => typeof(DaisyRadialProgress);

        public DaisyRadialProgress()
        {
            Minimum = 0;
            Maximum = 100;
            Value = 0;
        }

        public static readonly StyledProperty<DaisyProgressVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyRadialProgress, DaisyProgressVariant>(nameof(Variant), DaisyProgressVariant.Default);

        public DaisyProgressVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyRadialProgress, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly StyledProperty<double> ThicknessProperty =
            AvaloniaProperty.Register<DaisyRadialProgress, double>(nameof(Thickness), 4); // Default 10% roughly?

        public double Thickness
        {
            get => GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }
    }
}
